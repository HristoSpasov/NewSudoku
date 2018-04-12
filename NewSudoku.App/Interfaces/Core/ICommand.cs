namespace NewSudoku.App.Interfaces.Core
{
    public interface ICommand
    {
        void Execute(params string[] args);
    }
}