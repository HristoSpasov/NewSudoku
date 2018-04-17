namespace NewSudoku.App.Interfaces.Factories
{
    using NewSudoku.App.Interfaces.Strategies;

    public interface IInGameValueStrategyFactory
    {
        IGameValueStrategy GetConcreteStrategy(string keyStr);
    }
}