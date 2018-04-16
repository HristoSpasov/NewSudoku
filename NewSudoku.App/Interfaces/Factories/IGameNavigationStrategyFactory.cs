namespace NewSudoku.App.Interfaces.Factories
{
    using NewSudoku.App.Interfaces.Strategies;

    public interface IGameNavigationStrategyFactory
    {
        IGameNavigationStrategy GetStrategy(string inputKeyString);
    }
}
