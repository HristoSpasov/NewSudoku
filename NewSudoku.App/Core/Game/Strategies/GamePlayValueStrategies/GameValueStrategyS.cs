namespace NewSudoku.App.Core.Game.Strategies.GamePlayValueStrategies
{
    using NewSudoku.App.Interfaces.Strategies;

    public class GameValueStrategyS : IGameValueStrategy
    {
        public char GetValue()
        {
            return 'S';
        }
    }
}