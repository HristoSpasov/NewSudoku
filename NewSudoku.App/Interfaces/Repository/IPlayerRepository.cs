namespace NewSudoku.App.Interfaces.Repository
{
    using NewSudoku.Entities;
    using NewSudoku.Repository;

    public interface IPlayerRepository : IRepository<Player, string>
    {
    }
}