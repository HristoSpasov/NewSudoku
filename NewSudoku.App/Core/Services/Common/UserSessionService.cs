namespace NewSudoku.App.Core.Services.Common
{
    using NewSudoku.Entities;
    using NewSudoku.Services.Interfaces;

    public class UserSessionService : IUserSessionService
    {
        public Player User { get; private set; }

        public bool IsLoggedIn()
        {
            return this.User != null;
        }

        public Player Login(Player player)
        {
            this.User = player;

            return this.User;
        }

        public void Logout()
        {
            this.User = null;
        }
    }
}