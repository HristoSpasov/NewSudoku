namespace NewSudoku.App.Core.Game.Strategies.GamePlayLetterAsciiStrategies
{
    using NewSudoku.App.Interfaces.Strategies;

    public class InGameLetterAsciiStrategyV : IInGameLetterAsciiStrategy
    {
        public char[,] GetNumberAscii()
        {
            return new char[,]
                    {
                            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
                            { ' ', ' ', '\\', ' ', ' ', '/', ' ', ' ' },
                            { ' ', ' ', ' ', '\\', '/', ' ', ' ', ' ' },
                            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }
                    };
        }
    }
}