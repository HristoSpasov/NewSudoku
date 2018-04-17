namespace NewSudoku.App.Core.Game.Strategies.GamePlayNumberAsciiStrategy
{
    using NewSudoku.App.Interfaces.Strategies;

    public class InGameNumberAsciiStrategy1 : IInGameNumberAsciiStrategy
    {
        public char[,] GetNumberAscii()
        {
            return new char[,]
                       {
                            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                            { ' ', ' ', ' ', '/', '|', ' ', ' ', ' ' },
                            { ' ', ' ', ' ', ' ', '|', ' ', ' ', ' ' },
                            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }
                       };
        }
    }
}