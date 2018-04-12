namespace NewSudoku.App.Core.Game.Fasades
{
    using System;
    using System.IO;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Fasades;
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.Services.Interfaces;

    public class GameSetUpFasade : IGameSetUpFasade
    {
        private readonly ISudokuGridGenerator sudokuGridGenerator;
        private readonly IFilePathFactory filePathFactory;
        private readonly IUserSessionService userSessionService;

        public GameSetUpFasade(IFilePathFactory filePathFactory, ISudokuGridGenerator sudokuGridGenerator, IUserSessionService userSessionService)
        {
            this.sudokuGridGenerator = sudokuGridGenerator;
            this.filePathFactory = filePathFactory;
            this.userSessionService = userSessionService;
        }

        public bool SetUpGame(string gameType)
        {
            char[][] grid = this.getGrid(gameType);
            Console.WriteLine("Setting up game! ToDo");
            return true;
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