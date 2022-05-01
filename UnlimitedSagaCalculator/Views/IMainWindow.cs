using UnlimitedSagaCalculator.Controllers;

namespace UnlimitedSagaCalculator.Views
{
    public interface IMainWindow
    {
        void ShowWindow();
        void SetController(IMainController controller);
        void DisplayDamageCalculatorView();
        void UpdateLanguage();
    }
}
