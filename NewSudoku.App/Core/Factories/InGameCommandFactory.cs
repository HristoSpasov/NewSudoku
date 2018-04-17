namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Game;
    using Unity;

    public class InGameCommandFactory : IInGameCommandFactory
    {
        private readonly IUnityContainer unityContainer;

        public InGameCommandFactory(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public IInGameCommand Create(string cmdId)
        {
            if (this.unityContainer.IsRegistered<IInGameCommand>(cmdId))
            {
                return this.unityContainer.Resolve<IInGameCommand>(cmdId);
            }

            return null;
        }
    }
}