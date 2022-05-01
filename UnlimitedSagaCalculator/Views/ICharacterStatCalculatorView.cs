using UnlimitedSagaCalculator.Controllers;

namespace UnlimitedSagaCalculator.Views
{
    public interface ICharacterStatCalculatorView
    {
        void SetController(ICharacterStatCalculatorController controller);
        void UpdateLanguage();
    }
}
