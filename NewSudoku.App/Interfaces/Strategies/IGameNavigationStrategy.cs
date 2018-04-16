namespace NewSudoku.App.Interfaces.Strategies
{
    using NewSudoku.Services.Interfaces;

    public interface IGameNavigationStrategy
    {
        void MoveNavigation(INavigationService navigation);
    }
}
