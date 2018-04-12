namespace NewSudoku.App.Core.Game
{
    using System;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.App.Interfaces.Strategies;
    using NewSudoku.Services.Interfaces;

    public class ConsoleMenuPagination : IMenuPagination
    {
        private const int StartIndex = 0;
        private const string TerminateString = "Enter";

        private readonly IReaderService reader;
        private readonly IWriterService writer;
        private readonly IInterfaceService interfaceService;
        private readonly IMenuNavigation menuNavigation;
        private readonly IMenuNavigationStrategyFactory menuNavigationStrategyFactory;

        private string selected;

        public ConsoleMenuPagination(IReaderService reader, IWriterService writer, IInterfaceService interfaceService, IMenuNavigation menuNavigation, IMenuNavigationStrategyFactory menuNavigationStrategyFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.interfaceService = interfaceService;
            this.menuNavigation = menuNavigation;
            this.menuNavigationStrategyFactory = menuNavigationStrategyFactory;
        }

        public void Paginate(params string[] options)
        {
            this.clearConsole();

            int pointer = StartIndex;

            while (true)
            {
                this.setConsoleDefaultColors();
                this.clearConsole();

                for (int i = 0; i < (this.interfaceService.GetCanvasHeight() - options.Length) / 2; i++)
                {
                    this.writer.WriteLine(default(string));
                }

                for (int i = 0; i < options.Length; i++)
                {
                    this.setConsoleDefaultColors();

                    if (pointer == Array.IndexOf(options, options[i]))
                    {
                        this.setConsoleSelectedOptionColors();
                    }

                    this.writer.Write(new string(' ', (this.interfaceService.GetCanvasWidth() - options[i].Length) / 2) + options[i] + new string(' ', (this.interfaceService.GetCanvasWidth() - options[i].Length) / 2));
                    this.selected = options[pointer];
                }

                string pressedKey = this.reader.ReadKeyboardInput();

                if (pressedKey == TerminateString)
                {
                    break;
                }

                IMenuNavigationStrategy strategy = this.menuNavigationStrategyFactory.Create(pressedKey);

                if (strategy == null)
                {
                    continue; // If another key is pressed
                }

                this.menuNavigation.SetMoveStrategy(strategy);
                pointer = this.menuNavigation.Move(pointer);

                if (pointer > options.Length - 1)
                {
                    pointer = StartIndex;
                }
                else if (pointer < StartIndex)
                {
                    pointer = options.Length - 1;
                }

                this.setConsoleDefaultColors();
            }
        }

        public string GetSelectedOption()
        {
            return this.selected;
        }

        private void setConsoleDefaultColors()
        {
            this.interfaceService.SetBackgroundColor("black");
            this.interfaceService.SetForegroundColor("white");
        }

        private void setConsoleSelectedOptionColors()
        {
            this.interfaceService.SetBackgroundColor("white");
            this.interfaceService.SetForegroundColor("black");
        }

        private void clearConsole()
        {
            this.interfaceService.Clear();
        }
    }
}