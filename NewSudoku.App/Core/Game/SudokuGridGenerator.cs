namespace NewSudoku.App.Core.Game
{
    using System;
    using System.Linq;
    using NewSudoku.App.Constants;
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.Services.Interfaces;

    public class SudokuGridGenerator : ISudokuGridGenerator
    {
        private readonly IFileService fileService;

        public SudokuGridGenerator(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public char[][] Generate(string filePath)
        {
            char[][] grid = new char[SudokuConstants.Size][];

            string gridAsString = this.fileService.ReadFile(filePath);

            int gridLineCounter = 0;

            gridAsString
                .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
                .Select(e => string.Join(string.Empty, e.Split(',')))
                .Select(e => e.ToCharArray())
                .ToList()
                .ForEach(e => grid[gridLineCounter++] = e);

            return grid;
        }
    }
}