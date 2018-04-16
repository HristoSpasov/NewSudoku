namespace NewSudoku.Entities
{
    using System;
    using System.Collections.Generic;

    public class Game
    {
        private string gameType;
        private char[][] pattern;
        private char[][] allBoardChars;
        private DateTime startTime;
        private DateTime endTime;
        private Field[] fields;

        public Game(string gameType, char[][] pattern, char[][] allBoardChars, DateTime startTime, Field[] fields)
        {
            this.gameType = gameType;
            this.pattern = pattern;
            this.allBoardChars = allBoardChars;
            this.startTime = startTime;
            this.fields = this.fields = fields;
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

        public IReadOnlyCollection<char[]> BoardChars
        {
            get
            {
                return this.allBoardChars;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }

            set
            {
                if (value < this.StartTime)
                {
                    throw new ArgumentException("End time cannot be before start time.");
                }

                this.endTime = value;
            }
        }

        public IReadOnlyCollection<Field> Fields
        {
            get
            {
                return this.fields;
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

        public void UpdateFieldValue(int positionY, int positionX, char newValue)
        {
            throw new NotImplementedException(); // ToDo
        }
    }
}