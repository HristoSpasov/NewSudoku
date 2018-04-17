namespace NewSudoku.App.Constants
{
    public static class ButtonsConstants
    {
        public const string CheckId = "Check";
        public const int CheckMinRow = 7;
        public const int CheckMinCol = 92;
        public const int CheckMaxRow = 13;
        public const int CheckMaxCol = 122;
        public const string SolutionIsCorrectMsg = "Correct! Press any key to exit.";
        public const string SolutionIsInCorrectMsg = "Incorrect! Press any key to continue game";

        public const string SolutionId = "Solution";
        public const int SolutionMinRow = 18;
        public const int SolutionMinCol = 92;
        public const int SolutionMaxRow = 24;
        public const int SolutionMaxCol = 137;
        public const string SolutionPromptMessage = "Press 'y' to get solution! Game'll stop!";
        public const string PostSolutionMessage = "Press any key to go to Main Menu";

        public const string ExitId = "Exit";
        public const int ExitMinRow = 29;
        public const int ExitMinCol = 92;
        public const int ExitMaxRow = 35;
        public const int ExitMaxCol = 119;
        public const string ExitPromptMessage = "Press 'y' for exit or any other key to continue!";
    }
}