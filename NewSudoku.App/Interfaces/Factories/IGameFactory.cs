namespace NewSudoku.App.Interfaces.Factories
{
    using System;
    using NewSudoku.Entities;

    public interface IGameFactory
    {
        Game Create(string gameType, char[][] pattern, char[] availableChars, char[][] board, DateTime startTime, Field[] fields, Button[] buttons);
    }
}