namespace NewSudoku.App.Core.Services
{
    using System;
    using NewSudoku.Services.Interfaces;

    public class ConsoleWriterService : IWriterService
    {
        public void Write(string msg)
        {
            Console.Write(msg);
        }

        public void WriteChar(char ch)
        {
            Console.Write(ch);
        }

        public void WriteLine(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}