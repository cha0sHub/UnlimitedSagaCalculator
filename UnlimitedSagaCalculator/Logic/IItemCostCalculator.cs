using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Logic
{
    public interface IItemCostCalculator
    {
        ItemCostResult CalculateItemCost(ItemData itemData);
    }
}
