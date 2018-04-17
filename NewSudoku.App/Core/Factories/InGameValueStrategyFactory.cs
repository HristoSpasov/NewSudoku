namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Strategies;
    using Unity;

    public class InGameValueStrategyFactory : IInGameValueStrategyFactory
    {
        private readonly IUnityContainer unityContainer;

        public InGameValueStrategyFactory(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public IGameValueStrategy GetConcreteStrategy(string keyStr)
        {
            if (this.unityContainer.IsRegistered<IGameValueStrategy>(keyStr))
            {
                return this.unityContainer.Resolve<IGameValueStrategy>(keyStr);
            }

            return null;
        }
    }
}