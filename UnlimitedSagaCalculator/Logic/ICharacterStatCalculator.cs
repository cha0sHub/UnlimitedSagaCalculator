using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Logic
{
    public interface ICharacterStatCalculator
    {
        CharacterStatResult CalculateCharacterStats(CharacterData characterData, PanelBoard panelBoard);
    }
}
