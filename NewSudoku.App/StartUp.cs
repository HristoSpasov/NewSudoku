namespace NewSudoku.App
{
    using NewSudoku.App.Core;
    using NewSudoku.App.Core.Commands;
    using NewSudoku.App.Core.Factories;
    using NewSudoku.App.Core.Game;
    using NewSudoku.App.Core.Game.Fasades;
    using NewSudoku.App.Core.Repository;
    using NewSudoku.App.Core.Services;
    using NewSudoku.App.Core.Services.Common;
    using NewSudoku.App.Interfaces.Core;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Fasades;
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.App.Interfaces.Repository;
    using NewSudoku.App.Utilities.Menus;
    using NewSudoku.Services.Interfaces;
    using Unity;

    public class StartUp
    {
        public static void Main()
        {
            IUnityContainer dependencyContainer = defineDependencyContainer();

            IEngine engine = dependencyContainer.Resolve<Engine>();
            engine.Run();

            // IPlayerRepository playerXMLRepository = dependencyContainer.Resolve<PlayerXMLRepository>();

            // Player player = new Player
            // {
            //    Id = "Pesho"
            // };

            // player.Points = 81;

            // playerXMLRepository.Insert(player);

            // player.Game.Fields.Add(new Field() { Id = 5000 });
        }

        private static IUnityContainer defineDependencyContainer()
        {
            IUnityContainer dependencyContainer = new UnityContainer();

            // Application
            dependencyContainer.RegisterType<IEngine, Engine>();

            // Repository
            dependencyContainer.RegisterType<IPlayerRepository, PlayerXMLRepository>();

            // Register menu options
            dependencyContainer.RegisterInstance<AbstractMenu>("Main Menu", new MainMenuOptions());
            dependencyContainer.RegisterInstance<AbstractMenu>("New Game", new GameSelectMenuOptions());
            dependencyContainer.RegisterInstance<AbstractMenu>("Report", new ReportMenuOptions());

            // Services
            dependencyContainer.RegisterType<IFileService, FileService>();
            dependencyContainer.RegisterType<IInterfaceService, ConsoleInterfaceService>();
            dependencyContainer.RegisterType<IMenuNavigation, ConsoleMenuNavigation>();
            dependencyContainer.RegisterType<IMenuPagination, ConsoleMenuPagination>();
            dependencyContainer.RegisterType<IMenuService, ConsoleMenuService>();
            dependencyContainer.RegisterType<IReaderService, ConsoleReaderService>();
            dependencyContainer.RegisterType<IWriterService, ConsoleWriterService>();
            dependencyContainer.RegisterType<IUserService, UserService>();
            dependencyContainer.RegisterType<IMenuNavigationStrategyFactory, MenuNavigationStrategyFactory>();
            dependencyContainer.RegisterType<ISudokuGridGenerator, SudokuGridGenerator>();
            dependencyContainer.RegisterType<INavigationService, ConsoleNavigationService>();

            // Fasade dependencies
            dependencyContainer.RegisterType<IGamePlayFasade, GamePlayFasade>();
            dependencyContainer.RegisterType<IGameSetUpFasade, GameSetUpFasade>();

            // User session
            dependencyContainer.RegisterInstance<IUserSessionService>(new UserSessionService());

            // Factories
            dependencyContainer.RegisterType<ICommandFactory, CommandFactory>();
            dependencyContainer.RegisterType<IFilePathFactory, FilePathFactory>();

            // Commands
            dependencyContainer.RegisterType<ICommand, GameCommand>("Letter Game");
            dependencyContainer.RegisterType<ICommand, GameCommand>("Number Game");

            // Register container
            dependencyContainer.RegisterInstance(dependencyContainer);

            return dependencyContainer;
        }
    }
}