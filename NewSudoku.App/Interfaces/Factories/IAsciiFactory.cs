namespace NewSudoku.App.Interfaces.Factories
{
    public interface IAsciiFactory
    {
        char[,] GetAsciiCharacter(string charStr);
    }
}