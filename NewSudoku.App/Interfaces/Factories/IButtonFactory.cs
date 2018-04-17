namespace NewSudoku.App.Interfaces.Factories
{
    using NewSudoku.Entities;

    public interface IButtonFactory
    {
        Button Create(string id, int minCol, int minRow, int maxCol, int maxRow);
    }
}