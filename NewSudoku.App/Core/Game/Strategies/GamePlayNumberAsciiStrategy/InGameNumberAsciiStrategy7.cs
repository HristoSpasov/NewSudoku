namespace NewSudoku.App.Core.Game.Strategies.GamePlayNumberAsciiStrategy
{
    using NewSudoku.App.Interfaces.Strategies;

    public class InGameNumberAsciiStrategy7 : IInGameNumberAsciiStrategy
    {
        public char[,] GetNumberAscii()
        {
            return new char[,]
                        {
                            { ' ', ' ', ' ', '_', '_', '_', ' ', ' ' },
                            { ' ', ' ', ' ', ' ', ' ', '/', ' ', ' ' },
                            { ' ', ' ', ' ', ' ', '/', ' ', ' ', ' ' },
                            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }
                        };
        }
    }
}