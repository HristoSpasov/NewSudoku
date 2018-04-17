namespace NewSudoku.App.Core.Game.InGameCommands
{
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.Services.Interfaces;

    public class ExitGameCommand : IInGameCommand
    {
        private readonly IUserSessionService userSessionService;
        private readonly IInterfaceService interfaceService;
        private readonly IWriterService writerService;
        private readonly IReaderService readerService;
        private readonly IUserService userService;

        public ExitGameCommand(IUserSessionService userSessionService, IInterfaceService interfaceService, IWriterService writerService, IReaderService readerService, IUserService userService)
        {
            this.userSessionService = userSessionService;
            this.interfaceService = interfaceService;
            this.writerService = writerService;
            this.readerService = readerService;
            this.userService = userService;
        }

        public void Execute()
        {
            this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
            this.interfaceService.SetForegroundColor(NavigationConstants.DefaultColor);
            this.writerService.Write(ButtonsConstants.ExitPromptMessage);
            string pressedKey = this.readerService.ReadKeyboardInput();

            if(pressedKey.ToLower() == "y")
            {
                this.userSessionService.User.TotalGamesPlayed++;
                this.userSessionService.User.TotalTimePlayed += this.userSessionService.User.Game.GamePlaySeconds;

                this.userService.Update();

                this.userSessionService.User.Game.IsRunning = false;
            }
            else
            {
                this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
                for (int i = 0; i < ButtonsConstants.ExitPromptMessage.Length; i++)
                {
                    this.writerService.WriteChar('\0');
                }
            }
        }
    }
}