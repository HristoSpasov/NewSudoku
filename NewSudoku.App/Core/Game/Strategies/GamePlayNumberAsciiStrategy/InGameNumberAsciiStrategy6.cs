﻿namespace NewSudoku.App.Core.Game.Strategies.GamePlayNumberAsciiStrategy
{
    using NewSudoku.App.Interfaces.Strategies;

    public class InGameNumberAsciiStrategy6 : IInGameNumberAsciiStrategy
    {
        public char[,] GetNumberAscii()
        {
            return new char[,]
                        {
                            { ' ', ' ', ' ', '_', '_', ' ', ' ', ' ' },
                            { ' ', ' ', '/', '_', '_', ' ', ' ', ' ' },
                            { ' ', ' ', '\\', '_', '_', ')', ' ', ' ' },
                            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' }
                        };
        }
    }
}