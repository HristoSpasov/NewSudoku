namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Strategies;
    using Unity;

    public class InGameLetterAsciiStrategyFactory : IInGameLetterAsciiStrategyFactory
    {
        private readonly IUnityContainer unityContainer;

        public InGameLetterAsciiStrategyFactory(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public IInGameLetterAsciiStrategy GetStrategy(string charStr)
        {
            if (this.unityContainer.IsRegistered<IInGameLetterAsciiStrategy>(charStr))
            {
                return this.unityContainer.Resolve<IInGameLetterAsciiStrategy>(charStr);
            }

            return null;
        }
    }
}