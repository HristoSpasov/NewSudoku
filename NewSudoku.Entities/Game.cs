namespace NewSudoku.Entities
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private string gameType;
        private char[][] pattern;
        private char[] availableCharsForPlay;
        private char[][] allBoardChars;
        private DateTime startTime;
        private Field[] fields;
        private Button[] buttons;

        public Game(string gameType, char[][] pattern, char[] availableCharsForPlay, char[][] allBoardChars, DateTime startTime, Field[] fields, Button[] buttons)
        {
            this.gameType = gameType;
            this.pattern = pattern;
            this.availableCharsForPlay = availableCharsForPlay;
            this.allBoardChars = allBoardChars;
            this.startTime = startTime;
            this.fields = fields;
            this.buttons = buttons;
        }

        public string GameType
        {
            get
            {
                return this.gameType;
            }
        }

        public IReadOnlyCollection<char[]> Pattern
        {
            get
            {
                return this.pattern;
            }
        }

        public IReadOnlyCollection<char> AvailavleChars
        {
            get
            {
                return this.availableCharsForPlay;
            }
        }

        public IReadOnlyCollection<char[]> BoardChars
        {
            get
            {
                return this.allBoardChars;
            }
        }

        public IReadOnlyCollection<Field> Fields
        {
            get
            {
                return this.fields;
            }
        }

        public IReadOnlyCollection<Button> Buttons
        {
            get
            {
                return this.buttons;
            }
        }

        public bool IsRunning { get; set; }

        public long GamePlaySeconds
        {
            get
            {
                return (long)DateTime.Now.Subtract(this.startTime).TotalSeconds;
            }
        }

        public char GetBoardChar(int positionX, int positionY)
        {
            if (positionY < 0 ||
                positionX < 0 ||
                positionY > this.allBoardChars.Length - 1 ||
                positionX > this.allBoardChars[positionY].Length - 1)
            {
                throw new ArgumentException("Cannot get an elemnt outside of board bounds!");
            }

            return this.allBoardChars[positionY][positionX];
        }

        public void SetBoardChar(char newChar, int positionX, int positionY)
        {
            if (positionY < 0 ||
                positionX < 0 ||
                positionY > this.allBoardChars.Length - 1 ||
                positionX > this.allBoardChars[positionY].Length - 1)
            {
                throw new ArgumentException("Cannot set an elemnt outside of board bounds!");
            }

            this.allBoardChars[positionY][positionX] = newChar;
        }
    }
}