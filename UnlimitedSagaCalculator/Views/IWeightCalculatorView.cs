using UnlimitedSagaCalculator.Controllers;

namespace UnlimitedSagaCalculator.Views
{
    public interface IWeightCalculatorView
    {
        void SetController(IWeightCalculatorController controller);
        void UpdateLanguage();
    }
}
