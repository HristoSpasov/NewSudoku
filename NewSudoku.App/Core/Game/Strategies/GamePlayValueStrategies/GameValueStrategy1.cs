namespace NewSudoku.App.Core.Game.Strategies.GamePlayValueStrategies
{
    using NewSudoku.App.Interfaces.Strategies;

    public class GameValueStrategy1 : IGameValueStrategy
    {
        public char GetValue()
        {
            return '1';
        }
    }
}