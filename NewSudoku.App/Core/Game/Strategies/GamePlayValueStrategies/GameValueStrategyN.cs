﻿namespace NewSudoku.App.Core.Game.Strategies.GamePlayValueStrategies
{
    using NewSudoku.App.Interfaces.Strategies;

    public class GameValueStrategyN : IGameValueStrategy
    {
        public char GetValue()
        {
            return 'N';
        }
    }
}