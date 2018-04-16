namespace NewSudoku.App.Core.Game.Fasades
{
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Fasades;
    using NewSudoku.App.Interfaces.Strategies;
    using NewSudoku.Services.Interfaces;

    public class GamePlayFasade : IGamePlayFasade
    {
        private readonly IReaderService readerService;
        private readonly IWriterService writerService;
        private readonly IInterfaceService interfaceService;
        private readonly IUserSessionService userSessionService;
        private readonly INavigationService navigationService;
        private readonly IGameNavigationStrategyFactory gameNavigationStrategyFactory;

        public GamePlayFasade(IReaderService readerService, IWriterService writerService, IInterfaceService interfaceService, IUserSessionService userSessionService, INavigationService navigationService, IGameNavigationStrategyFactory gameNavigationStrategyFactory)
        {
            this.readerService = readerService;
            this.writerService = writerService;
            this.interfaceService = interfaceService;
            this.userSessionService = userSessionService;
            this.navigationService = navigationService;
            this.gameNavigationStrategyFactory = gameNavigationStrategyFactory;
        }

        public void Play()
        {
            while (true)
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

                if(gameNavigationStrategy == null && pressedKey == "Enter")
                {
                    System.Console.WriteLine("a");
                }
                else if(gameNavigationStrategy != null)
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
    }
}
