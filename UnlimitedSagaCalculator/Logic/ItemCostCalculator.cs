using System.Linq;
using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Logic
{
    internal class ItemCostCalculator : IItemCostCalculator
    {
        private IUsagaModel UsagaModel { get; }
        public ItemCostCalculator(IUsagaModel usagaModel)
        {
            UsagaModel = usagaModel;
        }

        public ItemCostResult CalculateItemCost(ItemData itemData)
        {
            
            var material = UsagaModel.MaterialCollection.Materials.First(m => m.EnglishName == itemData.Material);

            var baseValue = material.BaseValue * .2;

            var equipmentModifier = material.BaseValue;
            if (itemData.Type == "Head")
            {
                equipmentModifier *= .6;
            }
            else if (itemData.Type == "Chest")
            {
                equipmentModifier *= .75;
            }
            else if (itemData.Type == "Leg")
            {
                equipmentModifier *= .45;
            }
            else if (itemData.Type == "Material")
            {
                equipmentModifier *= .3;
            }
            else
            {
                equipmentModifier *= itemData.Durability / 100;
            }

            var abilityModifier = material.BaseValue;
            if (itemData.NumberOfAbilities == 1)
            {
                abilityModifier *= .05;
            }
            else if (itemData.NumberOfAbilities == 2)
            {
                abilityModifier *= .2;
            }
            else if (itemData.NumberOfAbilities == 3)
            {
                abilityModifier *= .45;
            }
            else
            {
                abilityModifier *= .8;
            }

            var result = new ItemCostResult
            {
                BaseValue = baseValue,
                EquipmentModifier = equipmentModifier,
                AbilityModifier = abilityModifier,
                FinalCost = (int)(baseValue + equipmentModifier + abilityModifier)
            };

            return result;
        }
    }
}
