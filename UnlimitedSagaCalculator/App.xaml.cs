using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using UnlimitedSagaCalculator.Controllers;
using UnlimitedSagaCalculator.DependencyInjection;

namespace UnlimitedSagaCalculator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServices();
            var serviceProvider = serviceCollection.BuildServiceProvider();

            var mainController = serviceProvider.GetRequiredService<IMainController>();

            mainController.LoadInitialSettings();
            mainController.ShowWindow();
        }
    }
}
