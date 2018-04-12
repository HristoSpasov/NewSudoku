namespace NewSudoku.App.Utilities.Menus
{
    internal abstract class AbstractMenu
    {
        private string[] options;

        protected AbstractMenu(params string[] options)
        {
            this.Options = options;
        }

        public string[] Options
        {
            get
            {
                return this.options;
            }

            protected set
            {
                this.options = value;
            }
        }
    }
}