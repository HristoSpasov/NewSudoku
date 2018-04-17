namespace NewSudoku.App.Interfaces.Factories
{
    using NewSudoku.App.Interfaces.Game;

    public interface IInGameCommandFactory
    {
        IInGameCommand Create(string cmdId);
    }
}