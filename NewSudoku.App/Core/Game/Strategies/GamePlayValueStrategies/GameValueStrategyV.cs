﻿namespace NewSudoku.App.Core.Game.Strategies.GamePlayValueStrategies
{
    using NewSudoku.App.Interfaces.Strategies;

    public class GameValueStrategyV : IGameValueStrategy
    {
        public char GetValue()
        {
            return 'V';
        }
    }
}