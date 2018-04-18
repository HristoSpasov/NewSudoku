namespace NewSudoku.App.Core.Game.Strategies.GamePlayValueStrategies
{
    using NewSudoku.App.Interfaces.Strategies;

    public class GameValueStrategyO : IGameValueStrategy
    {
        public char GetValue()
        {
            return 'O';
        }
    }
}