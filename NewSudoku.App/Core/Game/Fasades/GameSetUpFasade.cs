﻿namespace NewSudoku.App.Core.Game.Fasades
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Fasades;
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.Entities;
    using NewSudoku.Services.Interfaces;

    public class GameSetUpFasade : IGameSetUpFasade
    {
        private readonly ISudokuGridGenerator sudokuGridGenerator;
        private readonly IFilePathFactory filePathFactory;
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;
        private readonly IGameFactory gameFactory;
        private readonly IInterfaceService interfaceService;
        private readonly IWriterService writerService;
        private readonly IAsciiFactoriesFactory asciiFactoriesFactory;
        private readonly IFieldFactory fieldFactory;

        public GameSetUpFasade(IFilePathFactory filePathFactory, ISudokuGridGenerator sudokuGridGenerator, IUserService userService, IUserSessionService userSessionService, IGameFactory gameFactory, IInterfaceService interfaceService, IWriterService writerService, IAsciiFactoriesFactory asciiFactoriesFactory, IFieldFactory fieldFactory)
        {
            this.sudokuGridGenerator = sudokuGridGenerator;
            this.filePathFactory = filePathFactory;
            this.userSessionService = userSessionService;
            this.userService = userService;
            this.gameFactory = gameFactory;
            this.interfaceService = interfaceService;
            this.writerService = writerService;
            this.asciiFactoriesFactory = asciiFactoriesFactory;
            this.fieldFactory = fieldFactory;
        }

        public bool SetUpGame(string gameType)
        {
            char[][] pattern = this.getGrid(gameType); // Get random pattern
            char[][] board = this.getBoard(); // Read board 

            Field[] fields = this.generateFields(gameType, pattern); // Generate field objects
            this.mapFieldDataToBoard(board, fields); // Map field content to board content

            Game newGame = this.gameFactory.Create(gameType, pattern, board, DateTime.Now, fields);
            this.userSessionService.User.SetGame(newGame); // Set the instance of a game

            this.drawBoard(board); // Draw board

            return true; // Setup success
        }

        private void drawBoard(char[][] board)
        {
            for (int row = 0; row < board.Length; row++)
            {
                for (int col = 0; col < board[row].Length; col++)
                {
                    this.interfaceService.SetCoordinates(row, col);
                    this.writerService.WriteChar(board[row][col]);
                }
            }
        }

        private void mapFieldDataToBoard(char[][] board, Field[] fields)
        {
            foreach (var field in fields)
            {
                if (!field.IsImmutable)
                {
                    field.Content[0, 0] = FieldConstants.EmptyFieldSymbol;
                }

                int maxRow = field.MinRow + FieldConstants.FieldRows;
                int maxCol = field.MinCol + FieldConstants.FieldCols;

                for (int row = field.MinRow, contentRow = 0; row < maxRow; row++, contentRow++)
                {
                    for (int col = field.MinCol, contentCol = 0; col < maxCol; col++, contentCol++)
                    {
                        board[row][col] = field.Content[contentRow, contentCol];
                    }
                }
            }
        }

        private Field[] generateFields(string gameType, char[][] generatedSudoku)
        {
            IAsciiFactory asciiFactory = this.asciiFactoriesFactory.Create(gameType);

            int topLeftFieldStartRow = FieldConstants.FieldMinY;

            int offset = 1; // Used to overcome borders

            List<Field> fields = new List<Field>();

            // Loop over the generated array of digits and create new field object
            for (int row = 0; row < generatedSudoku.Length; row++)
            {
                int topLeftFieldStartCol = FieldConstants.FieldMinX;

                for (int col = 0; col < generatedSudoku[row].Length; col++)
                {
                    char[,] asciiSymbol = asciiFactory.GetAsciiCharacter(generatedSudoku[row][col]);
                    Field newField = this.fieldFactory.Create(topLeftFieldStartCol, topLeftFieldStartRow, asciiSymbol, generatedSudoku[row][col], generatedSudoku[row][col] != '0', row, col);

                    if (newField.Value == 0)
                    {
                        newField.Content[0, 0] = FieldConstants.EmptyFieldSymbol;
                    }

                    fields.Add(newField);
                    topLeftFieldStartCol += FieldConstants.FieldCols + offset;
                }

                topLeftFieldStartRow += FieldConstants.FieldRows + offset;
            }

            return fields.ToArray();
        }

        private char[][] getBoard()
        {
            string boardPath = GamePaths.BoardGameReferentPath;

            char[][] board = new char[BoardConstants.Height][];

            using (StreamReader reader = new StreamReader(boardPath))
            {
                int boardRowCounter = 0;

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    board[boardRowCounter++] = line.ToCharArray();
                }
            }

            return board;
        }

        private char[][] getGrid(string gameType)
        {
            string allPatternsPath = this.filePathFactory.CreatePath(gameType); // Get path to corresponding game patterns

            string[] patterns = Directory.GetFiles(allPatternsPath);

            int patternId = this.getRandomId(patterns.Length);
            string patternPath = Path.GetFullPath(Path.Combine(allPatternsPath, patterns[patternId]));

            return this.sudokuGridGenerator.Generate(patternPath);
        }

        private int getRandomId(int length)
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(0, length);

            return randomNum;
        }
    }
}