namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.Entities;

    public class FieldFactory : IFieldFactory
    {
        public Field Create(int minCol, int minRow, char[,] content, char value, bool isImmutable, int boardRow, int boardCol)
        {
            return new Field(minCol, minRow, content, value, isImmutable, boardRow, boardCol);
        }
    }
}
