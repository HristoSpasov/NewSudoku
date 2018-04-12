namespace NewSudoku.Services.Interfaces
{
    public interface IFileService
    {
        string ReadFile(string filePath);

        void WriteFile(string filePath, string content);
    }
}