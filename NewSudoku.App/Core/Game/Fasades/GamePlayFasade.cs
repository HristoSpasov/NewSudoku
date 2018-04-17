namespace NewSudoku.App.Core.Game.Fasades
{
    using System.Linq;
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Fasades;
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.App.Interfaces.Strategies;
    using NewSudoku.Entities;
    using NewSudoku.Services.Interfaces;

    public class GamePlayFasade : IGamePlayFasade
    {
        private readonly IReaderService readerService;
        private readonly IWriterService writerService;
        private readonly IInterfaceService interfaceService;
        private readonly IUserSessionService userSessionService;
        private readonly INavigationService navigationService;
        private readonly IGameNavigationStrategyFactory gameNavigationStrategyFactory;
        private readonly IAsciiFactoriesFactory asciiFactoriesFactory;
        private readonly IInGameValueStrategyFactory gameValueStrategyFactory;
        private readonly IInGameCommandFactory inGameCommandFactory;

        public GamePlayFasade(IReaderService readerService, IWriterService writerService, IInterfaceService interfaceService, IUserSessionService userSessionService, INavigationService navigationService, IGameNavigationStrategyFactory gameNavigationStrategyFactory, IAsciiFactoriesFactory asciiFactoriesFactory, IInGameValueStrategyFactory gameValueStrategyFactory, IInGameCommandFactory inGameCommandFactory)
        {
            this.readerService = readerService;
            this.writerService = writerService;
            this.interfaceService = interfaceService;
            this.userSessionService = userSessionService;
            this.navigationService = navigationService;
            this.gameNavigationStrategyFactory = gameNavigationStrategyFactory;
            this.asciiFactoriesFactory = asciiFactoriesFactory;
            this.gameValueStrategyFactory = gameValueStrategyFactory;
            this.inGameCommandFactory = inGameCommandFactory;
        }

        public void Play(string gameType)
        {
            this.userSessionService.User.Game.IsRunning = true;

            while (this.userSessionService.User.Game.IsRunning)
            {
                this.interfaceService.SetForegroundColor(ConsoleConstants.ForegroundColor.ToString());
                this.interfaceService.SetBackgroundColor(ConsoleConstants.BackgroundColor.ToString());

                string pressedKey = this.readerService.ReadKeyboardInput();
                this.interfaceService.SetCoordinates(this.navigationService.PositionY, this.navigationService.PositionX);

                int oldPositionX = this.navigationService.PositionX;
                int oldPositionY = this.navigationService.PositionY;
                char charToRefreshAfterMove = this.userSessionService
                    .User
                    .Game
                    .GetBoardChar(this.navigationService.PositionX, this.navigationService.PositionY);

                this.writerService.WriteChar('\0'); // Clear char

                IGameNavigationStrategy gameNavigationStrategy = this.gameNavigationStrategyFactory.GetStrategy(pressedKey);

                if (gameNavigationStrategy == null && pressedKey == "Enter")
                {
                    this.resolveAction(gameType);
                }
                else if (gameNavigationStrategy != null)
                {
                    gameNavigationStrategy.MoveNavigation(this.navigationService);
                }

                this.interfaceService.SetCoordinates(oldPositionY, oldPositionX);
                this.writerService.WriteChar(charToRefreshAfterMove);
                this.interfaceService.SetCoordinates(this.navigationService.PositionY, this.navigationService.PositionX);
                this.interfaceService.SetForegroundColor(NavigationConstants.DefaultColor);
                this.writerService.WriteChar(NavigationConstants.Symbol);
            }
        }

        private void resolveAction(string gameType)
        {
            Field fieldMatch = this.searchField();
            Button buttonMatch = this.searchButton();

            if (fieldMatch != null)
            {
                this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
                this.interfaceService.SetForegroundColor(BoardConstants.DefaultPromptColor);
                this.writerService.Write(BoardConstants.PromptUserForInput);

                string pressedKey = this.readerService.ReadKeyboardInput().Last().ToString();

                IAsciiFactory asciiFactory = this.asciiFactoriesFactory.Create(gameType);
                char[,] asciiContent = asciiFactory.GetAsciiCharacter(pressedKey);

                IGameValueStrategy concreteValueStrategy = this.gameValueStrategyFactory.GetConcreteStrategy(pressedKey);

                // Validate and update field
                if (asciiContent != null && concreteValueStrategy != null)
                {
                    char value = concreteValueStrategy.GetValue();

                    fieldMatch.Value = value;
                    fieldMatch.Content = asciiContent;

                    this.setDefaultColors();
                    this.updateBoard(fieldMatch);
                    this.drawNewField(fieldMatch);
                }

                this.setDefaultColors();
                this.clearUserPrompt();
            }
            else if (buttonMatch != null)
            {
                string commandName = buttonMatch.Id;

                IInGameCommand command = this.inGameCommandFactory.Create(commandName);
                command?.Execute();
            }
        }

        private void clearUserPrompt()
        {
            this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);

            for (int i = 0; i < BoardConstants.PromptUserForInput.Length; i++)
            {
                this.writerService.WriteChar('\0');
            }
        }

        private void updateBoard(Field field)
        {
            int boardRow = field.MinRow;
            int boardCol = field.MinCol;

            field.Content[0, 0] = FieldConstants.EmptyFieldSymbol; // Mark mutable field

            for (int fieldRow = 0; fieldRow < FieldConstants.FieldRows; fieldRow++)
            {
                for (int fieldCol = 0; fieldCol < FieldConstants.FieldCols; fieldCol++)
                {
                    this.userSessionService.User.Game.SetBoardChar(field.Content[fieldRow, fieldCol], boardCol, boardRow);

                    boardCol++;
                }

                boardRow++;
                boardCol = field.MinCol;
            }
        }

        private void drawNewField(Field field)
        {
            int boardRow = field.MinRow;
            int boardCol = field.MinCol;

            for (int fieldRow = 0; fieldRow < FieldConstants.FieldRows; fieldRow++)
            {
                for (int fieldCol = 0; fieldCol < FieldConstants.FieldCols; fieldCol++)
                {
                    this.interfaceService.SetCoordinates(boardRow, boardCol);
                    this.writerService.WriteChar(field.Content[fieldRow, fieldCol]);

                    boardCol++;
                }

                boardRow++;
                boardCol = field.MinCol;
            }
        }

        private Field searchField()
        {
            int offset = 1; // To exclude border

            Field field = this.userSessionService
                .User.Game.Fields
                .Where(f => !f.IsImmutable)
                .SingleOrDefault(f => this.navigationService.PositionX >= f.MinCol &&
                                      this.navigationService.PositionX <= f.MinCol + FieldConstants.FieldCols - offset &&
                                      this.navigationService.PositionY >= f.MinRow &&
                                      this.navigationService.PositionY <= f.MinRow + FieldConstants.FieldRows - offset);

            return field;
        }

        private Button searchButton()
        {
            Button button = this.userSessionService
                .User
                .Game
                .Buttons
                .SingleOrDefault(b => this.navigationService.PositionX >= b.MinCol &&
                                      this.navigationService.PositionX <= b.MaxCol &&
                                      this.navigationService.PositionY >= b.MinRow &&
                                      this.navigationService.PositionY <= b.MaxRow);

            return button;
        }

        private void setDefaultColors()
        {
            this.interfaceService.SetForegroundColor(ConsoleConstants.ForegroundColor);
            this.interfaceService.SetBackgroundColor(ConsoleConstants.BackgroundColor);
        }
    }
}