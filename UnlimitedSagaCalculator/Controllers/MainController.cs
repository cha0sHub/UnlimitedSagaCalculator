using System.Globalization;
using System.Threading;
using UnlimitedSagaCalculator.Configuration;
using UnlimitedSagaCalculator.Views;

namespace UnlimitedSagaCalculator.Controllers
{
    internal class MainController : IMainController
    {
        private IMainWindow MainWindow { get; }
        private IDamageCalculatorController DamageCalculatorController { get; }
        private ICharacterStatCalculatorController CharacterStatCalculatorController { get; }
        private IWeightCalculatorController WeightCalculatorController { get; }
        private IItemCostCalculatorController ItemCostCalculatorController { get; }
        private IConfigurationManager ConfigurationManager { get; }

        public MainController(IMainWindow mainWindow, IDamageCalculatorController damageCalculatorController, ICharacterStatCalculatorController characterStatCalculatorController, IWeightCalculatorController weightCalculatorController, IItemCostCalculatorController itemCostCalculatorController, IConfigurationManager configurationManager)
        {
            MainWindow = mainWindow;
            DamageCalculatorController = damageCalculatorController;
            CharacterStatCalculatorController = characterStatCalculatorController;
            WeightCalculatorController = weightCalculatorController;
            ItemCostCalculatorController = itemCostCalculatorController;
            ConfigurationManager = configurationManager;
            
            MainWindow.SetController(this);
            CharacterStatCalculatorController.SetMainController(this);
        }

        public void ShowWindow()
        {
            MainWindow.ShowWindow();
        }

        public void LoadInitialSettings()
        {
            ConfigurationManager.LoadConfiguration();
            if (!string.IsNullOrEmpty(ConfigurationManager.UserSettings.Language))
            {
                SetLanguage(ConfigurationManager.UserSettings.Language);
            }
            else
            {
                var currentCulture = CultureInfo.CurrentCulture;
                SetLanguage(currentCulture.Name);
            }
        }

        public void SetLanguage(string languageCode)
        {
            var newCulture = new CultureInfo(languageCode);
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;
            DamageCalculatorController.UpdateLanguage();
            CharacterStatCalculatorController.UpdateLanguage();
            WeightCalculatorController.UpdateLanguage();
            ItemCostCalculatorController.UpdateLanguage();
            MainWindow.UpdateLanguage();
            ConfigurationManager.UserSettings.Language = languageCode;
            ConfigurationManager.SaveConfiguration();
        }

        public void DisplayDamageCalculatorView()
        {
            MainWindow.DisplayDamageCalculatorView();
        }
    }
}
