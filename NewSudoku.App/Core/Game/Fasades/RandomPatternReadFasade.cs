namespace NewSudoku.App.Core.Game.Fasades
{
    using System;
    using System.IO;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Fasades;
    using NewSudoku.App.Interfaces.Game;

    public class RandomPatternReadFasade : IRandomPatternReadFasade
    {
        private readonly IFilePathFactory filePathFactory;
        private readonly ISudokuGridGenerator sudokuGridGenerator;

        public RandomPatternReadFasade(IFilePathFactory filePathFactory, ISudokuGridGenerator sudokuGridGenerator)
        {
            this.filePathFactory = filePathFactory;
            this.sudokuGridGenerator = sudokuGridGenerator;
        }

        public char[][] GetPattern(string gameType)
        {
            return this.getGrid(gameType);
        }

        private char[][] getGrid(string gameType)
        {
            string allPatternsPath = this.filePathFactory.CreatePath(gameType); // Get path to corresponding game patterns

            string[] patterns = Directory.GetFiles(allPatternsPath);

            int patternId = this.getRandomId(patterns.Length);
            string patternPath = Path.GetFullPath(Path.Combine(allPatternsPath, patterns[patternId]));

            return this.sudokuGridGenerator.Generate(patternPath);
        }

        private int getRandomId(int length)
        {
            Random rnd = new Random();
            int randomNum = rnd.Next(0, length);

            return randomNum;
        }
    }
}