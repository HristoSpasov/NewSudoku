namespace NewSudoku.App.Core.Game
{
    using System.Collections.Generic;
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Game;

    public class SudokuGridSolver : ISudokuGridSolver
    {
        private char[][] grid;

        public IReadOnlyCollection<char[]> GetGrid
        {
            get
            {
                return this.grid;
            }
        }

        public bool SolveSudoku(char[][] grid)
        {
            if (this.grid == null)
            {
                this.grid = grid;
            }

            for (int row = 0; row < SudokuConstants.Size; row++)
            {
                for (int col = 0; col < SudokuConstants.Size; col++)
                {
                    if (this.grid[row][col] == '0')
                    {
                        for (int num = SudokuConstants.MinNum; num <= SudokuConstants.MaxNum; num++)
                        {
                            if (this.isSafe(row, col, char.Parse(num.ToString())))
                            {
                                this.grid[row][col] = char.Parse(num.ToString());

                                if (this.SolveSudoku(grid))
                                {
                                    return true;
                                }

                                this.grid[row][col] = '0';
                            }
                        }

                        return false;
                    }
                }
            }

            return true;
        }

        private bool usedInBox(int row, int col, char num)
        {
            int boxStartRow = row - (row % SudokuConstants.BoxSize);
            int boxStartCol = col - (col % SudokuConstants.BoxSize);

            for (int r = 0; r < SudokuConstants.BoxSize; r++)
            {
                for (int c = 0; c < SudokuConstants.BoxSize; c++)
                {
                    if (this.grid[r + boxStartRow][c + boxStartCol] == num)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool usedInCol(int col, char num)
        {
            for (int r = 0; r < this.grid[col].Length; r++)
            {
                if (this.grid[r][col] == num)
                {
                    return true;
                }
            }

            return false;
        }

        private bool usedInRow(int row, char num)
        {
            for (int c = 0; c < this.grid.Length; c++)
            {
                if (this.grid[row][c] == num)
                {
                    return true;
                }
            }

            return false;
        }

        private bool isSafe(int row, int col, char num)
        {
            return !this.usedInRow(row, num) && !this.usedInCol(col, num) && !this.usedInBox(row, col, num);
        }
    }
}