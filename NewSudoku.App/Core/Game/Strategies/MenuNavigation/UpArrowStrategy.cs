namespace NewSudoku.App.Core.Game.Strategies.MenuNavigation
{
    using NewSudoku.App.Interfaces.Strategies;

    public class UpArrowStrategy : IMenuNavigationStrategy
    {
        public UpArrowStrategy()
        {
        }

        public int MovePointer(int pointer)
        {
            return --pointer;
        }
    }
}