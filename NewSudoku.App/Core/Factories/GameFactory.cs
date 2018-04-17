namespace NewSudoku.App.Core.Factories
{
    using System;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.Entities;

    public class GameFactory : IGameFactory
    {
        public Game Create(string gameType, char[][] pattern, char[][] board, DateTime startTime, Field[] fields, Button[] buttons)
        {
            return new Game(gameType, pattern, board, startTime, fields, buttons);
        }
    }
}