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
            // this.game = new Game();
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
    }
}