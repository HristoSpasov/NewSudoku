namespace NewSudoku.App.Interfaces.Factories
{
    using NewSudoku.App.Interfaces.Strategies;

    public interface IMenuNavigationStrategyFactory
    {
        IMenuNavigationStrategy Create(string btnAsString);
    }
}