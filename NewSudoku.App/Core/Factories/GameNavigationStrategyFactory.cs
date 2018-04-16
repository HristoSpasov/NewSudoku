namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Strategies;
    using Unity;

    public class GameNavigationStrategyFactory : IGameNavigationStrategyFactory
    {
        private readonly IUnityContainer unityContainer;

        public GameNavigationStrategyFactory(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public IGameNavigationStrategy GetStrategy(string inputKeyString)
        {
            if (this.unityContainer.IsRegistered<IGameNavigationStrategy>(inputKeyString))
            {
                return this.unityContainer.Resolve<IGameNavigationStrategy>(inputKeyString);
            }

            return null;
        }
    }
}
