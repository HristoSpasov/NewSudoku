namespace NewSudoku.App.Interfaces.Factories
{
    public interface IAsciiFactoriesFactory
    {
        IAsciiFactory Create(string concreteFactory);
    }
}