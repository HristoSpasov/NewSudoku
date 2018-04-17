namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Strategies;

    public class AsciiNumberFactory : IAsciiFactory
    {
        private readonly IInGameNumberAsciiStrategyFactory inGameNumberAsciiStrategyFactory;

        public AsciiNumberFactory(IInGameNumberAsciiStrategyFactory inGameNumberAsciiStrategyFactory)
        {
            this.inGameNumberAsciiStrategyFactory = inGameNumberAsciiStrategyFactory;
        }

        public char[,] GetAsciiCharacter(string charStr)
        {
            IInGameNumberAsciiStrategy concreteStrategy = this.inGameNumberAsciiStrategyFactory.GetStrategy(charStr);

            if (concreteStrategy != null)
            {
                return concreteStrategy.GetNumberAscii();
            }

            return null;
        }
    }
}