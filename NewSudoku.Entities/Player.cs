namespace NewSudoku.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Serializable]
    [Table("Player")]
    public class Player : IEntity<string>
    {
        [NonSerialized]
        private Game game;

        public Player()
        {
        }

        [Key]
        public string Id { get; set; }

        public int Points { get; set; }

        public int TotalGamesPlayed { get; set; }

        public Game Game
        {
            get
            {
                return this.game;
            }
        }

        public void SetGame(Game game)
        {
            this.game = game;
        }
    }
}