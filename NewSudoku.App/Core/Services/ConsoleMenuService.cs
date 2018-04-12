namespace NewSudoku.App.Core.Services
{
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.Services.Interfaces;

    public class ConsoleMenuService : IMenuService
    {
        private readonly IMenuPagination pagination;

        public ConsoleMenuService(IMenuPagination pagination)
        {
            this.pagination = pagination;
        }

        public string MenuRender(params string[] options)
        {
            this.pagination.Paginate(options);
            return this.pagination.GetSelectedOption();
        }
    }
}