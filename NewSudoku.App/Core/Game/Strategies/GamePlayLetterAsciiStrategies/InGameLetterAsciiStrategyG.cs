namespace NewSudoku.App.Core.Game.Strategies.GamePlayLetterAsciiStrategies
{
    using NewSudoku.App.Interfaces.Strategies;

    public class InGameLetterAsciiStrategyG : IInGameLetterAsciiStrategy
    {
        public char[,] GetNumberAscii()
        {
            return new char[,]
                    {
                            { ' ', ' ', ' ', ' ', '_', ' ', ' ', ' ' },
                            { ' ', ' ', ' ', '/', ' ', '_', ' ', ' ' },
                            { ' ', ' ', ' ', '\\', '_', '/', ' ', ' ' },
                            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }
                    };
        }
    }
}