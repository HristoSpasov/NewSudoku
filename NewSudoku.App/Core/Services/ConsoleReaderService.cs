namespace NewSudoku.App.Core.Services
{
    using System;
    using NewSudoku.Services.Interfaces;

    public class ConsoleReaderService : IReaderService
    {
        public string ReadKeyboardInput()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);

            return key.Key.ToString().Trim();
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}