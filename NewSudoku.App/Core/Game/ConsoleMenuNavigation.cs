namespace NewSudoku.App.Core.Game
{
    using NewSudoku.App.Interfaces.Game;
    using NewSudoku.App.Interfaces.Strategies;

    public class ConsoleMenuNavigation : IMenuNavigation
    {
        private IMenuNavigationStrategy menuNavigationStrategy;

        public int Move(int oldPointer)
        {
            return this.menuNavigationStrategy.MovePointer(oldPointer);
        }

        public void SetMoveStrategy(IMenuNavigationStrategy moveStrategy)
        {
            this.menuNavigationStrategy = moveStrategy;
        }
    }
}