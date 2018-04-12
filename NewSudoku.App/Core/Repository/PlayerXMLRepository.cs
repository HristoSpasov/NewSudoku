namespace NewSudoku.App.Core.Repository
{
    using NewSudoku.App.Interfaces.Repository;
    using NewSudoku.Entities;
    using NewSudoku.Repository.BaseRepositories;
    using NewSudoku.Repository.Source;

    public class PlayerXMLRepository : XMLBaseRepository<XMLSet<Player>, Player, string>, IPlayerRepository
    {
        public PlayerXMLRepository()
            : base("players.xml")
        {
        }
    }
}