namespace NewSudoku.App.Core.Game.InGameCommands
{
    using System;
    using System.Linq;
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Fasades;
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.Services.Interfaces;

    public class SolutionGameCommand : IInGameCommand
    {
        private readonly IUserSessionService userSessionService;
        private readonly ISudokuGridSolver sudokuGridSolver;
        private readonly IInterfaceService interfaceService;
        private readonly IReaderService readerService;
        private readonly IWriterService writerService;
        private readonly IUserService userService;
        private readonly IGameSetUpFasade gameSetUpFasade;

        public SolutionGameCommand(IUserSessionService userSessionService, ISudokuGridSolver sudokuGridSolver, IInterfaceService interfaceService, IReaderService readerService, IWriterService writerService, IUserService userService, IGameSetUpFasade gameSetUpFasade)
        {
            this.userSessionService = userSessionService;
            this.sudokuGridSolver = sudokuGridSolver;
            this.interfaceService = interfaceService;
            this.readerService = readerService;
            this.writerService = writerService;
            this.userService = userService;
            this.gameSetUpFasade = gameSetUpFasade;
        }

        public void Execute()
        {
            this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
            this.interfaceService.SetForegroundColor(NavigationConstants.DefaultColor);
            this.writerService.Write(ButtonsConstants.SolutionPromptMessage);

            string pressedKey = this.readerService.ReadKeyboardInput();

            if (pressedKey.ToLower() == "y")
            {
                char[][] pattern = this.userSessionService.User.Game.Pattern.ToArray();

                if (this.sudokuGridSolver.SolveSudoku(pattern))
                {
                    char[][] solvedGrid = this.sudokuGridSolver.GetGrid.ToArray();

                    this.interfaceService.SetForegroundColor(ConsoleConstants.ForegroundColor);
                    this.gameSetUpFasade.SetUpGame(this.userSessionService.User.Game.GameType, solvedGrid); // Generate board with all values filled in

                    this.userSessionService.User.TotalGamesPlayed++;
                    this.userSessionService.User.TotalTimePlayed += this.userSessionService.User.Game.GamePlaySeconds;
                    this.userService.Update();
                    this.userSessionService.User.Game.IsRunning = false;

                    this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
                    this.interfaceService.SetForegroundColor(NavigationConstants.DefaultColor);
                    this.writerService.Write(ButtonsConstants.PostSolutionMessage);
                    this.readerService.ReadKeyboardInput();
                }
                else
                {
                    throw new InvalidOperationException("Was unable to solve sudoku grid!");
                }
            }
            else
            {
                this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
                for (int i = 0; i < ButtonsConstants.SolutionPromptMessage.Length; i++)
                {
                    this.writerService.WriteChar('\0');
                }
            }
        }
    }
}