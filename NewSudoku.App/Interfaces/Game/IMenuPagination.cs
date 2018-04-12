namespace NewSudoku.App.Interfaces.Game
{
    public interface IMenuPagination
    {
        void Paginate(params string[] options);

        string GetSelectedOption();
    }
}