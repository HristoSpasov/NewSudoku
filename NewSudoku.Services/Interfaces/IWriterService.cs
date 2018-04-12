namespace NewSudoku.Services.Interfaces
{
    public interface IWriterService
    {
        void Write(string msg);

        void WriteLine(string msg);

        void WriteChar(char ch);
    }
}