namespace NewSudoku.App.Core.Game.Strategies.MenuNavigation
{
    using NewSudoku.App.Interfaces.Strategies;

    public class DownArrowStrategy : IMenuNavigationStrategy
    {
        public int MovePointer(int pointer)
        {
            return ++pointer;
        }
    }
}