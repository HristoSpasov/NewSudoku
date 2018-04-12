namespace NewSudoku.App.Core.Services.Common
{
    using System.IO;
    using System.Text;
    using NewSudoku.Services.Interfaces;

    public class FileService : IFileService
    {
        public string ReadFile(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            using (StreamReader reader = new StreamReader(filePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    sb.AppendLine(line);
                }
            }

            return sb.ToString().Trim();
        }

        public void WriteFile(string filePath, string content)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(content);
            }
        }
    }
}