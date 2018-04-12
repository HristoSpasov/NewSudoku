namespace NewSudoku.App.Interfaces.Game
{
    using NewSudoku.App.Interfaces.Strategies;

    public interface IMenuNavigation
    {
        void SetMoveStrategy(IMenuNavigationStrategy moveStrategy);

        int Move(int oldPosition);
    }
}