namespace NewSudoku.App.Core.Services
{
    using NewSudoku.App.Constants;
    using NewSudoku.Services.Interfaces;

    public class ConsoleNavigationService : INavigationService
    {
        private int positionX;
        private int positionY;

        public ConsoleNavigationService()
        {
            this.positionX = NavigationConstants.MinPositionX;
            this.positionY = NavigationConstants.MinPositionY;
        }

        public int PositionX
        {
            get
            {
                return this.positionX;
            }

            set
            {
                if (value >= NavigationConstants.MinPositionX && value <= NavigationConstants.MaxPositionX)
                {
                    this.positionX = value;
                }
            }
        }

        public int PositionY
        {
            get
            {
                return this.positionY;
            }

            set
            {
                if (value >= NavigationConstants.MinPositionY && value <= NavigationConstants.MaxtPositionY)
                {
                    this.positionY = value;
                }
            }
        }
    }
}