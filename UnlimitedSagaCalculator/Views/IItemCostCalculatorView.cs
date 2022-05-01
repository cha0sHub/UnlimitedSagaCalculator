using UnlimitedSagaCalculator.Controllers;

namespace UnlimitedSagaCalculator.Views
{
    public interface IItemCostCalculatorView
    {
        void SetController(IItemCostCalculatorController controller);
        void SetNumberOfAbilitiesVisibility(bool isVisible);
        void SetDurabilityVisibility(bool isVisible);
        void UpdateLanguage();
    }
}
