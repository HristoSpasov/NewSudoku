namespace NewSudoku.App.Core.Factories
{
    using System.IO;
    using NewSudoku.App.Interfaces.Factories;

    public class FilePathFactory : IFilePathFactory
    {
        public string CreatePath(string gameType)
        {
            string allPatternsPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Files\Patterns\"));

            if (gameType.Contains("Number"))
            {
                return allPatternsPath + "Number";
            }

            return allPatternsPath + "Letter";
        }
    }
}