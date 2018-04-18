namespace NewSudoku.App.Core.Commands
{
    using System;
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Core;
    using NewSudoku.Services.Interfaces;

    public class ExitCommand : ICommand
    {
        private readonly IReaderService readerService;
        private readonly IWriterService writerService;
        private readonly IInterfaceService interfaceService;

        public ExitCommand(IReaderService readerService, IWriterService writerService, IInterfaceService interfaceService)
        {
            this.readerService = readerService;
            this.writerService = writerService;
            this.interfaceService = interfaceService;
        }

        public void Execute(params string[] args)
        {
            this.interfaceService.SetCoordinates(BoardConstants.InformationRow, BoardConstants.InformationCol);
            this.interfaceService.SetBackgroundColor(ConsoleConstants.BackgroundColor);
            this.interfaceService.SetForegroundColor(NavigationConstants.DefaultColor);
            this.writerService.Write(ButtonsConstants.ExitPromptMessage);
            string pressedKey = this.readerService.ReadKeyboardInput();

            if (pressedKey.ToLower() == "y")
            {
                this.interfaceService.Clear();
                Environment.Exit(0);
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