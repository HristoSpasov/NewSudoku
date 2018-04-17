namespace NewSudoku.App.Core.Game.InGameCommands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.Entities;
    using NewSudoku.Services.Interfaces;

    public class CheckGameCommand : IInGameCommand
    {
        private readonly IUserSessionService userSessionService;
        private readonly ISudokuGridSolver sudokuGridSolver;
        private readonly IUserService userService;
        private readonly IWriterService writerService;
        private readonly IInterfaceService interfaceService;
        private readonly IReaderService readerService;

        public CheckGameCommand(IUserSessionService userSessionService, ISudokuGridSolver sudokuGridSolver, IUserService userService, IWriterService writerService, IInterfaceService interfaceService, IReaderService readerService)
        {
            this.userSessionService = userSessionService;
            this.sudokuGridSolver = sudokuGridSolver;
            this.userService = userService;
            this.writerService = writerService;
            this.interfaceService = interfaceService;
            this.readerService = readerService;
        }

        public void Execute()
        {
            IReadOnlyCollection<char[]> pattern = this.userSessionService
                .User
                .Game
                .Pattern;

            if (this.sudokuGridSolver.SolveSudoku(pattern.ToArray()))
            {
                char[][] solvedGrid = this.sudokuGridSolver.GetGrid.ToArray();
                Field[] currentUserSolution = this.userSessionService.User.Game.Fields.ToArray();

                if (this.solutionIsCorrect(solvedGrid, currentUserSolution))
                {
                    this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
                    this.interfaceService.SetForegroundColor(NavigationConstants.DefaultColor);
                    this.writerService.Write(ButtonsConstants.SolutionIsCorrectMsg);
                    this.readerService.ReadKeyboardInput();

                    this.userSessionService.User.Points++;
                    this.userSessionService.User.TotalGamesPlayed++;
                    this.userSessionService.User.TotalTimePlayed += this.userSessionService.User.Game.GamePlaySeconds;

                    this.userService.Update();

                    this.userSessionService.User.Game.IsRunning = false;
                }
                else
                {
                    this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
                    this.interfaceService.SetForegroundColor(NavigationConstants.DefaultColor);
                    this.writerService.Write(ButtonsConstants.SolutionIsInCorrectMsg);
                    this.readerService.ReadKeyboardInput();

                    this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
                    for (int i = 0; i < ButtonsConstants.SolutionIsInCorrectMsg.Length; i++)
                    {
                        this.writerService.WriteChar('\0');
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Unable to solve pattern!");
            }
        }

        private bool solutionIsCorrect(char[][] solvedGrid, Field[] fields)
        {
            for (int row = 0; row < solvedGrid.Length; row++)
            {
                for (int col = 0; col < solvedGrid[row].Length; col++)
                {
                    Field field = fields.SingleOrDefault(f => f.BoardRow == row && f.BoardCol == col);

                    if (field == null)
                    {
                        throw new ArgumentNullException("Field does not exist!");
                    }

                    if (solvedGrid[row][col] != field.Value)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}