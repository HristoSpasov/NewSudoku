namespace NewSudoku.App.Interfaces.Game
{
    public interface ISudokuGridGenerator
    {
        char[][] Generate(string filePath);
    }
}