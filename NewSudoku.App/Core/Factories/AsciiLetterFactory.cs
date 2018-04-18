namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Strategies;

    public class AsciiLetterFactory : IAsciiFactory
    {
        private readonly IInGameLetterAsciiStrategyFactory inGameLetterAsciiStrategyFactory;

        public AsciiLetterFactory(IInGameLetterAsciiStrategyFactory inGameLetterAsciiStrategyFactory)
        {
            this.inGameLetterAsciiStrategyFactory = inGameLetterAsciiStrategyFactory;
        }

        public char[,] GetAsciiCharacter(string ch)
        {
            IInGameLetterAsciiStrategy concreteStrategy = this.inGameLetterAsciiStrategyFactory.GetStrategy(ch);

            if (concreteStrategy != null)
            {
                return concreteStrategy.GetNumberAscii();
            }

            return null;
        }
    }
}