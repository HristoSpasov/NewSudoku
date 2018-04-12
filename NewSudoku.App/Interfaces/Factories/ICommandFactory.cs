namespace NewSudoku.App.Interfaces.Factories
{
    using NewSudoku.App.Interfaces.Core;

    public interface ICommandFactory
    {
        ICommand Create(string menuOption);
    }
}