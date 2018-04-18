namespace NewSudoku.App.Core.Game.Fasades
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Fasades;
    using NewSudoku.Entities;
    using NewSudoku.Services.Interfaces;

    public class GameSetUpFasade : IGameSetUpFasade
    {
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;
        private readonly IGameFactory gameFactory;
        private readonly IInterfaceService interfaceService;
        private readonly IWriterService writerService;
        private readonly IAsciiFactoriesFactory asciiFactoriesFactory;
        private readonly IFieldFactory fieldFactory;
        private readonly IButtonFactory buttonFactory;

        public GameSetUpFasade(IUserService userService, IUserSessionService userSessionService, IGameFactory gameFactory, IInterfaceService interfaceService, IWriterService writerService, IAsciiFactoriesFactory asciiFactoriesFactory, IFieldFactory fieldFactory, IButtonFactory buttonFactory)
        {
            this.userSessionService = userSessionService;
            this.userService = userService;
            this.gameFactory = gameFactory;
            this.interfaceService = interfaceService;
            this.writerService = writerService;
            this.asciiFactoriesFactory = asciiFactoriesFactory;
            this.fieldFactory = fieldFactory;
            this.buttonFactory = buttonFactory;
        }

        public bool SetUpGame(string gameType, char[][] pattern)
        {
            char[][] board = this.getBoard(); // Read board

            char[] availableCharsForPlay = this.getAvailableChars(pattern); // Read pattern and find all available chars for playing
            this.mapAvailableCharsOnBoard(board, availableCharsForPlay);

            Field[] fields = this.generateFields(gameType, pattern); // Generate field objects
            this.mapFieldDataToBoard(board, fields); // Map field content to board content

            Button[] buttons = this.generateButtons(); // Generate button data

            Game newGame = this.gameFactory.Create(gameType, pattern, availableCharsForPlay, board, DateTime.Now, fields, buttons);
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

        private Button[] generateButtons()
        {
            Button checkButton = this.buttonFactory.Create(ButtonsConstants.CheckId, ButtonsConstants.CheckMinCol, ButtonsConstants.CheckMinRow, ButtonsConstants.CheckMaxCol, ButtonsConstants.CheckMaxRow);
            Button solutionkButton = this.buttonFactory.Create(ButtonsConstants.SolutionId, ButtonsConstants.SolutionMinCol, ButtonsConstants.SolutionMinRow, ButtonsConstants.SolutionMaxCol, ButtonsConstants.SolutionMaxRow);
            Button exitButton = this.buttonFactory.Create(ButtonsConstants.ExitId, ButtonsConstants.ExitMinCol, ButtonsConstants.ExitMinRow, ButtonsConstants.ExitMaxCol, ButtonsConstants.ExitMaxRow);

            return new Button[] { checkButton, solutionkButton, exitButton };
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
                    char[,] asciiSymbol = asciiFactory.GetAsciiCharacter(generatedSudoku[row][col].ToString());
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

        private void mapAvailableCharsOnBoard(char[][] board, char[] availableCharsForPlay)
        {
            int startRow = BoardConstants.AvailableCharsRow;
            int startCol = BoardConstants.AvailableCharsCol;

            foreach (char ch in string.Join(", ", availableCharsForPlay))
            {
                board[startRow][startCol] = ch;
                startCol++;
            }
        }

        private char[] getAvailableChars(char[][] pattern)
        {
            HashSet<char> uniqueGameChars = new HashSet<char>();

            foreach (char[] row in pattern)
            {
                foreach (char ch in row)
                {
                    uniqueGameChars.Add(ch);
                }
            }

            return uniqueGameChars
                .Where(e => e != '0')
                .OrderBy(c => c)
                .ToArray();
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
    }
}