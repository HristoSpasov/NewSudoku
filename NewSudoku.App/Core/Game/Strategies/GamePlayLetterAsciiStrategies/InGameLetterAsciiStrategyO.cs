namespace NewSudoku.App.Core.Game.Strategies.GamePlayLetterAsciiStrategies
{
    using NewSudoku.App.Interfaces.Strategies;

    public class InGameLetterAsciiStrategyO : IInGameLetterAsciiStrategy
    {
        public char[,] GetNumberAscii()
        {
            return new char[,]
                     {
                            { ' ', ' ', ' ', ' ', '_', ' ', ' ', ' ' },
                            { ' ', ' ', ' ', '/', ' ', '\\', ' ', ' ' },
                            { ' ', ' ', ' ', '\\', '_', '/', ' ', ' ' },
                            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }
                     };
        }
    }
}