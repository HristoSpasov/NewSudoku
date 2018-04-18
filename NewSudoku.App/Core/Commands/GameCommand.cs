namespace NewSudoku.App.Core.Commands
{
    using System;
    using NewSudoku.App.Interfaces.Core;
    using NewSudoku.App.Interfaces.Fasades;

    public class GameCommand : ICommand
    {
        private readonly IGameSetUpFasade gameSetUp;
        private readonly IGamePlayFasade gamePlay;
        private readonly IRandomPatternReadFasade randomPatternReadFasade;

        public GameCommand(IGameSetUpFasade gameSetUp, IGamePlayFasade gamePlay, IRandomPatternReadFasade randomPatternReadFasade)
        {
            this.gameSetUp = gameSetUp;
            this.gamePlay = gamePlay;
            this.randomPatternReadFasade = randomPatternReadFasade;
        }

        public void Execute(params string[] args)
        {
            string gameType = args[0];

            char[][] pattern = this.randomPatternReadFasade.GetPattern(gameType); // Get random pattern

            if (!this.gameSetUp.SetUpGame(gameType, pattern))
            {
                throw new Exception("Game set up did not passed successfully!");
            }

            this.gamePlay.Play(gameType);
        }
    }
}