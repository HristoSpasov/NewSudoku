namespace NewSudoku.App.Core.Factories
{
    using System;
    using System.Linq;
    using System.Reflection;
    using NewSudoku.App.Interfaces.Factories;
    using NewSudoku.App.Interfaces.Strategies;

    public class MenuNavigationStrategyFactory : IMenuNavigationStrategyFactory
    {
        public IMenuNavigationStrategy Create(string btnAsString)
        {
            Type strategyType = Assembly.GetExecutingAssembly()
                 .GetTypes()
                 .Where(t => t.Name == btnAsString + "Strategy")
                 .SingleOrDefault();

            if (strategyType == null)
            {
                return null;
            }

            return (IMenuNavigationStrategy)Activator.CreateInstance(strategyType);
        }
    }
}