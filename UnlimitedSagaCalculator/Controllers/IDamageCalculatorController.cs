using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Controllers
{
    public interface IDamageCalculatorController
    {
        void CalculateHpDamage();
        void CalculateLpDamage();
        void UpdateLanguage();
        void TransferCharacterData(CharacterData characterData);
        void SetDamage();
        void AddToCombo();
    }
}
