namespace NewSudoku.Services.Interfaces
{
    using NewSudoku.Entities;

    public interface IUserSessionService
    {
        Player User { get; }

        Player Login(Player player);

        void Logout();

        bool IsLoggedIn();
    }
}