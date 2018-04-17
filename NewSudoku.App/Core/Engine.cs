namespace NewSudoku.App.Core
{
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Core;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Utilities.Menus;
    using NewSudoku.Services.Interfaces;
    using Unity;

    public class Engine : IEngine
    {
        private readonly IUnityContainer unityContainer;
        private readonly IInterfaceService interfaceService;
        private readonly IUserService userService;
        private readonly IUserSessionService userSessionService;
        private readonly ICommandFactory commandFactory;
        private readonly IMenuService menuService;
        private string currentOption = "Main Menu";

        public Engine(IUnityContainer unityContainer, IInterfaceService interfaceService, IUserService userService, IMenuService menuService, IUserSessionService userSessionService, ICommandFactory commandFactory)
        {
            this.unityContainer = unityContainer;
            this.interfaceService = interfaceService;
            this.userService = userService;
            this.menuService = menuService;
            this.userSessionService = userSessionService;
            this.commandFactory = commandFactory;
        }

        public void Run()
        {
            while (true)
            {
                this.interfaceService.SetCanvasSize(ConsoleConstants.Width, ConsoleConstants.Height);
                this.interfaceService.SetBackgroundColor(ConsoleConstants.BackgroundColor);
                this.interfaceService.SetForegroundColor(ConsoleConstants.ForegroundColor);

                if (!this.userSessionService.IsLoggedIn())
                {
                    this.userService.Login();
                }

                if (this.unityContainer.IsRegistered<AbstractMenu>(this.currentOption))
                {
                    this.currentOption = this.menuService.MenuRender(this.unityContainer.Resolve<AbstractMenu>(this.currentOption).Options);
                }
                else
                {
                    ICommand command = this.commandFactory.Create(this.currentOption);
                    command.Execute(this.currentOption);
                    this.currentOption = "Main Menu";
                }
            }
        }
    }
}