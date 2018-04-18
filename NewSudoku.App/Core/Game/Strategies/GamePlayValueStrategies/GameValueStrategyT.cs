namespace NewSudoku.App.Core.Game.Strategies.GamePlayValueStrategies
{
    using NewSudoku.App.Interfaces.Strategies;

    public class GameValueStrategyT : IGameValueStrategy
    {
        public char GetValue()
        {
            return 'T';
        }
    }
}