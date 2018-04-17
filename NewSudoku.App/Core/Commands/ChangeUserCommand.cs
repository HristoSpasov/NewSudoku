namespace NewSudoku.App.Core.Commands
{
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Core;
    using NewSudoku.Services.Interfaces;

    public class ChangeUserCommand : ICommand
    {
        private readonly IReaderService readerService;
        private readonly IWriterService writerService;
        private readonly IInterfaceService interfaceService;
        private readonly IUserService userService;

        public ChangeUserCommand(IReaderService readerService, IWriterService writerService, IInterfaceService interfaceService, IUserService userService)
        {
            this.readerService = readerService;
            this.writerService = writerService;
            this.interfaceService = interfaceService;
            this.userService = userService;
        }

        public void Execute(params string[] args)
        {
            this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
            this.interfaceService.SetBackgroundColor(ConsoleConstants.BackgroundColor);
            this.interfaceService.SetForegroundColor(NavigationConstants.DefaultColor);
            this.writerService.Write(Messages.GameLogoutPrompt);
            string pressedKey = this.readerService.ReadKeyboardInput();

            if (pressedKey.ToLower() == "y")
            {
                this.userService.Logout();
            }
            else
            {
                this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
                for (int i = 0; i < Messages.GameLogoutPrompt.Length; i++)
                {
                    this.writerService.WriteChar('\0');
                }
            }
        }
    }
}
