using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Logic
{
    public interface IWeightCalculator
    {
        WeightResult CalculateWeight(CharacterData characterData);
    }
}
