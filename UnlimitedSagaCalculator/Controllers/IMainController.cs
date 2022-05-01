namespace UnlimitedSagaCalculator.Controllers
{
    public interface IMainController
    {
        void ShowWindow();
        void SetLanguage(string languageCode);
        void DisplayDamageCalculatorView();
        void LoadInitialSettings();
    }
}
