﻿namespace NewSudoku.App.Core.Game.Strategies.GamePlayNavigationStrategies
{
    using NewSudoku.App.Interfaces.Strategies;
    using NewSudoku.Services.Interfaces;

    public class LeftArrowGameStrategy : IGameNavigationStrategy
    {
        public void MoveNavigation(INavigationService navigation)
        {
            navigation.PositionX--;
        }
    }
}