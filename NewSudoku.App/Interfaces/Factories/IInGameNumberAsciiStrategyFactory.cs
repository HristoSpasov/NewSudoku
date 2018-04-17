namespace NewSudoku.App.Interfaces.Factories
{
    using NewSudoku.App.Interfaces.Strategies;

    public interface IInGameNumberAsciiStrategyFactory
    {
        IInGameNumberAsciiStrategy GetStrategy(string numStr);
    }
}