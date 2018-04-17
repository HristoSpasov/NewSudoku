namespace NewSudoku.App.Interfaces.Fasades
{
    public interface IRandomPatternReadFasade
    {
        char[][] GetPattern(string gameType);
    }
}
