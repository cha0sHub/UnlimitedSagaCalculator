using UnlimitedSagaCalculator.Controllers;
using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Views
{
    public interface IDamageCalculatorView
    {
        void SetController(IDamageCalculatorController controller);
        void UpdateLanguage();
        void BoldMartialFields();
        void BoldStrengthFields();
        void BoldSkillFields();
        void BoldMagicFields(MagicType magicType);
    }
}
