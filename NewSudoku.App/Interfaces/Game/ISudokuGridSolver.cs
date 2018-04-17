namespace NewSudoku.App.Interfaces.Game
{
    using System.Collections.Generic;

    public interface ISudokuGridSolver
    {
        IReadOnlyCollection<char[]> GetGrid { get; }

        bool SolveSudoku(char[][] grid);
    }
}