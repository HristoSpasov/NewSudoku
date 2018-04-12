namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Core;
    using NewSudoku.App.Interfaces.Factories;
    using Unity;

    public class CommandFactory : ICommandFactory
    {
        private readonly IUnityContainer unityContainer;

        public CommandFactory(IUnityContainer unityContainer)
        {
            this.unityContainer = unityContainer;
        }

        public ICommand Create(string menuOption)
        {
            return this.unityContainer.Resolve<ICommand>(menuOption);
        }
    }
}