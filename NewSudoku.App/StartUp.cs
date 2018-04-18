namespace NewSudoku.App
{
    using NewSudoku.App.Constants;
    using NewSudoku.App.Core;
    using NewSudoku.App.Core.Commands;
    using NewSudoku.App.Core.Commands.ReportCommands;
    using NewSudoku.App.Core.Factories;
    using NewSudoku.App.Core.Game;
    using NewSudoku.App.Core.Game.Fasades;
    using NewSudoku.App.Core.Game.InGameCommands;
    using NewSudoku.App.Core.Game.Strategies.GamePlayLetterAsciiStrategies;
    using NewSudoku.App.Core.Game.Strategies.GamePlayNavigationStrategies;
    using NewSudoku.App.Core.Game.Strategies.GamePlayNumberAsciiStrategy;
    using NewSudoku.App.Core.Game.Strategies.GamePlayValueStrategies;
    using NewSudoku.App.Core.Repository;
    using NewSudoku.App.Core.Services;
    using NewSudoku.App.Core.Services.Common;
    using NewSudoku.App.Interfaces.Core;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Fasades;
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.App.Interfaces.Repository;
    using NewSudoku.App.Interfaces.Strategies;
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
            dependencyContainer.RegisterType<ISudokuGridSolver, SudokuGridSolver>();
            dependencyContainer.RegisterType<INavigationService, ConsoleNavigationService>();
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategyFactory, InGameNumberAsciiStrategyFactory>();
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategyFactory, InGameLetterAsciiStrategyFactory>();

            // Fasade dependencies
            dependencyContainer.RegisterType<IGamePlayFasade, GamePlayFasade>();
            dependencyContainer.RegisterType<IGameSetUpFasade, GameSetUpFasade>();
            dependencyContainer.RegisterType<IRandomPatternReadFasade, RandomPatternReadFasade>();

            // User session
            dependencyContainer.RegisterInstance<IUserSessionService>(new UserSessionService());

            // Factories
            dependencyContainer.RegisterType<ICommandFactory, CommandFactory>();
            dependencyContainer.RegisterType<IFilePathFactory, FilePathFactory>();
            dependencyContainer.RegisterType<IGameFactory, GameFactory>();
            dependencyContainer.RegisterType<IFieldFactory, FieldFactory>();
            dependencyContainer.RegisterType<IButtonFactory, ButtonFactory>();
            dependencyContainer.RegisterType<IAsciiFactoriesFactory, AsciiFactoriesFactory>();
            dependencyContainer.RegisterType<IAsciiFactory, AsciiNumberFactory>("Number Game");
            dependencyContainer.RegisterType<IAsciiFactory, AsciiLetterFactory>("Letter Game");
            dependencyContainer.RegisterType<IGameNavigationStrategyFactory, GameNavigationStrategyFactory>();
            dependencyContainer.RegisterType<IInGameValueStrategyFactory, InGameValueStrategyFactory>();
            dependencyContainer.RegisterType<IInGameCommandFactory, InGameCommandFactory>();

            // Menu Commands
            dependencyContainer.RegisterType<ICommand, GameCommand>("Letter Game");
            dependencyContainer.RegisterType<ICommand, GameCommand>("Number Game");
            dependencyContainer.RegisterType<ICommand, ExitCommand>("Exit");
            dependencyContainer.RegisterType<ICommand, ChangeUserCommand>("Change User");
            dependencyContainer.RegisterType<ICommand, ByHighScoreCommand>("By High Score");
            dependencyContainer.RegisterType<ICommand, ByHighScoreCommand>("By Total Games Played");
            dependencyContainer.RegisterType<ICommand, ByTotalTimePlayedCommand>("By Total Time Played");
            dependencyContainer.RegisterType<ICommand, ByUsernameCommand>("By Username");

            // In Game Commands
            dependencyContainer.RegisterType<IInGameCommand, CheckGameCommand>(ButtonsConstants.CheckId);
            dependencyContainer.RegisterType<IInGameCommand, SolutionGameCommand>(ButtonsConstants.SolutionId);
            dependencyContainer.RegisterType<IInGameCommand, ExitGameCommand>(ButtonsConstants.ExitId);

            // Game navigation strategies
            dependencyContainer.RegisterType<IGameNavigationStrategy, UpArrowGameStrategy>("UpArrow");
            dependencyContainer.RegisterType<IGameNavigationStrategy, DownArrowGameStrategy>("DownArrow");
            dependencyContainer.RegisterType<IGameNavigationStrategy, LeftArrowGameStrategy>("LeftArrow");
            dependencyContainer.RegisterType<IGameNavigationStrategy, RightArrowGameStrategy>("RightArrow");

            // Ascii number strategies
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategy, InGameNumberAsciiStrategy0>("0");
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategy, InGameNumberAsciiStrategy1>("1");
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategy, InGameNumberAsciiStrategy2>("2");
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategy, InGameNumberAsciiStrategy3>("3");
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategy, InGameNumberAsciiStrategy4>("4");
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategy, InGameNumberAsciiStrategy5>("5");
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategy, InGameNumberAsciiStrategy6>("6");
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategy, InGameNumberAsciiStrategy7>("7");
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategy, InGameNumberAsciiStrategy8>("8");
            dependencyContainer.RegisterType<IInGameNumberAsciiStrategy, InGameNumberAsciiStrategy9>("9");

            // Ascii letter strategies
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategy, InGameLetterAsciiStrategyC>("C");
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategy, InGameLetterAsciiStrategyE>("E");
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategy, InGameLetterAsciiStrategyG>("G");
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategy, InGameLetterAsciiStrategyI>("I");
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategy, InGameLetterAsciiStrategyN>("N");
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategy, InGameLetterAsciiStrategyO>("O");
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategy, InGameLetterAsciiStrategyS>("S");
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategy, InGameLetterAsciiStrategyT>("T");
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategy, InGameLetterAsciiStrategyV>("V");
            dependencyContainer.RegisterType<IInGameLetterAsciiStrategy, InGameLetterAsciiStrategy0>("0");

            // Value strategies
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategy0>("0");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategy1>("1");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategy2>("2");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategy3>("3");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategy4>("4");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategy5>("5");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategy6>("6");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategy7>("7");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategy8>("8");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategy9>("9");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategyC>("C");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategyE>("E");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategyG>("G");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategyI>("I");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategyN>("N");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategyO>("O");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategyS>("S");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategyT>("T");
            dependencyContainer.RegisterType<IGameValueStrategy, GameValueStrategyV>("V");

            // Register container
            dependencyContainer.RegisterInstance(dependencyContainer);

            return dependencyContainer;
        }
    }
}