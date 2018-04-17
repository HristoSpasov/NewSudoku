namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Factories;
    using Unity;

    public class AsciiFactoriesFactory : IAsciiFactoriesFactory
    {
        private readonly IUnityContainer unityContainer;

        public AsciiFactoriesFactory(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public IAsciiFactory Create(string concreteFactory)
        {
            return this.unityContainer.Resolve<IAsciiFactory>(concreteFactory);
        }
    }
}