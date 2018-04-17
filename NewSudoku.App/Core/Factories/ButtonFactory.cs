namespace NewSudoku.App.Core.Factories
{
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.Entities;

    public class ButtonFactory : IButtonFactory
    {
        public Button Create(string id, int minCol, int minRow, int maxCol, int maxRow)
        {
            return new Button(id, minCol, minRow, maxCol, maxRow);
        }
    }
}