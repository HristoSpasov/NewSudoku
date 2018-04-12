namespace NewSudoku.App.Core.Services
{
    using System;
    using NewSudoku.Services.Interfaces;

    public class ConsoleInterfaceService : IInterfaceService
    {
        public void SetCanvasSize(int width, int height)
        {
            Console.CursorVisible = false;
            Console.WindowHeight = height;
            Console.WindowWidth = width;
        }

        public int GetCanvasWidth()
        {
            return Console.WindowWidth;
        }

        public int GetCanvasHeight()
        {
            return Console.WindowHeight;
        }

        public void SetBackgroundColor(string color)
        {
            Console.BackgroundColor = this.parseColor(color);
        }

        public void SetForegroundColor(string color)
        {
            Console.ForegroundColor = this.parseColor(color);
        }

        public void Clear()
        {
            Console.Clear();
        }

        private ConsoleColor parseColor(string color)
        {
            string formatedColorString = char.ToUpperInvariant(color[0]).ToString() + color.Substring(1).ToLowerInvariant();

            if (!Enum.TryParse<ConsoleColor>(formatedColorString, out ConsoleColor result))
            {
                throw new ArgumentException($"Invalid console color selected: {color}");
            }

            return result;
        }
    }
}