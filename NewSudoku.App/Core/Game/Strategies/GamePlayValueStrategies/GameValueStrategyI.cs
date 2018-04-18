namespace NewSudoku.App.Core.Game.Strategies.GamePlayValueStrategies
{
    using NewSudoku.App.Interfaces.Strategies;

    public class GameValueStrategyI : IGameValueStrategy
    {
        public char GetValue()
        {
            return 'I';
        }
    }
}