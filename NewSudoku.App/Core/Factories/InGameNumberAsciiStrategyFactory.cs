namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Strategies;
    using Unity;

    public class InGameNumberAsciiStrategyFactory : IInGameNumberAsciiStrategyFactory
    {
        private readonly IUnityContainer unityContainer;

        public InGameNumberAsciiStrategyFactory(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public IInGameNumberAsciiStrategy GetStrategy(string numStr)
        {
            if (this.unityContainer.IsRegistered<IInGameNumberAsciiStrategy>(numStr))
            {
                return this.unityContainer.Resolve<IInGameNumberAsciiStrategy>(numStr);
            }

            return null;
        }
    }
}