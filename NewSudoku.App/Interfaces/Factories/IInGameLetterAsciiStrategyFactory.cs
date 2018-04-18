namespace NewSudoku.App.Interfaces.Factories
{
    using NewSudoku.App.Interfaces.Strategies;

    public interface IInGameLetterAsciiStrategyFactory
    {
        IInGameLetterAsciiStrategy GetStrategy(string charStr);
    }
}