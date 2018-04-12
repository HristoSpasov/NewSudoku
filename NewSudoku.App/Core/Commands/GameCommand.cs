namespace NewSudoku.App.Core.Commands
{
    using System;
    using NewSudoku.App.Interfaces.Core;
    using NewSudoku.App.Interfaces.Fasades;

    public class GameCommand : ICommand
    {
        private readonly IGameSetUpFasade gameSetUp;
        private readonly IGamePlayFasade gamePlay;

        public GameCommand(IGameSetUpFasade gameSetUp, IGamePlayFasade gamePlay)
        {
            this.gameSetUp = gameSetUp;
            this.gamePlay = gamePlay;
        }

        public void Execute(params string[] args)
        {
            string gameType = args[0];

            if (!this.gameSetUp.SetUpGame(gameType))
            {
                throw new Exception("Game set up did not passed successfully!");
            }

            this.gamePlay.Play();
        }
    }
}