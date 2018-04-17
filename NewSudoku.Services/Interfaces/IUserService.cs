namespace NewSudoku.Services.Interfaces
{
    public interface IUserService
    {
        string Login();

        void Logout();

        void Update();
    }
}