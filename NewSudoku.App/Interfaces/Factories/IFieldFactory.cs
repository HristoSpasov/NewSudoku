namespace NewSudoku.App.Interfaces.Factories
{
    using NewSudoku.Entities;

    public interface IFieldFactory
    {
        Field Create(int minCol, int minRow, char[,] content, char value, bool isImmutable, int boardRow, int boardCol);
    }
}
