namespace NewSudoku.Services.Interfaces
{
    public interface IInterfaceService
    {
        void SetCanvasSize(int width, int height);

        int GetCanvasWidth();

        int GetCanvasHeight();

        void SetBackgroundColor(string color);

        void SetForegroundColor(string color);

        void Clear();

        void SetCoordinates(int positionY, int positionX);
    }
}