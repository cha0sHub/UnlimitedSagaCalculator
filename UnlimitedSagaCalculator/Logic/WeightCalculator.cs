using System.Linq;
using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Logic
{
    internal class WeightCalculator : IWeightCalculator
    {

        private IUsagaModel UsagaModel { get; set; }

        public WeightCalculator(IUsagaModel usagaModel)
        {
            UsagaModel = usagaModel;
        }

        public WeightResult CalculateWeight(CharacterData characterData)
        {
            var weightResult = new WeightResult
            {
                CharacterWeight = characterData.Weight / 4
            };
            var equipmentWeight = 0.0;

            if (!string.IsNullOrEmpty(characterData.WeaponOneMaterial) && !string.IsNullOrEmpty(characterData.WeaponOneType))
            {
                var weaponOneWeight = GetItemWeight(characterData.WeaponOneMaterial, characterData.WeaponOneType);
                weightResult.WeaponOneWeight = weaponOneWeight / 8;
                equipmentWeight += weaponOneWeight;
            }
            if (!string.IsNullOrEmpty(characterData.WeaponTwoMaterial) && !string.IsNullOrEmpty(characterData.WeaponTwoType))
            {
                var weaponTwoWeight = GetItemWeight(characterData.WeaponTwoMaterial, characterData.WeaponTwoType);
                weightResult.WeaponTwoWeight = weaponTwoWeight / 8;
                equipmentWeight += weaponTwoWeight;
            }
            if (!string.IsNullOrEmpty(characterData.AccessoryOneMaterial) && !string.IsNullOrEmpty(characterData.AccessoryOneType))
            {
                var accessoryOneWeight = GetItemWeight(characterData.AccessoryOneMaterial, characterData.AccessoryOneType);
                weightResult.AccessoryOneWeight = accessoryOneWeight / 8;
                equipmentWeight += accessoryOneWeight;
            }
            if (!string.IsNullOrEmpty(characterData.AccessoryTwoMaterial) && !string.IsNullOrEmpty(characterData.AccessoryTwoType))
            {
                var accessoryTwoWeight = GetItemWeight(characterData.AccessoryTwoMaterial, characterData.AccessoryTwoType);
                weightResult.AccessoryTwoWeight = accessoryTwoWeight / 8;
                equipmentWeight += accessoryTwoWeight;
            }
            if (!string.IsNullOrEmpty(characterData.HeadMaterial) && !string.IsNullOrEmpty(characterData.HeadType))
            {
                var headWeight = GetItemWeight(characterData.HeadMaterial, characterData.HeadType);
                weightResult.HeadWeight = headWeight / 8;
                equipmentWeight += headWeight;
            }
            if (!string.IsNullOrEmpty(characterData.ChestMaterial) && !string.IsNullOrEmpty(characterData.ChestType))
            {
                var chestWeight = GetItemWeight(characterData.ChestMaterial, characterData.ChestType);
                weightResult.ChestWeight = chestWeight / 8;
                equipmentWeight += chestWeight;
            }
            if (!string.IsNullOrEmpty(characterData.LegsMaterial) && !string.IsNullOrEmpty(characterData.LegsType))
            {
                var legWeight = GetItemWeight(characterData.LegsMaterial, characterData.LegsType);
                weightResult.LegWeight = legWeight / 8;
                equipmentWeight += legWeight;
            }

            weightResult.TotalWeight = (int)((characterData.Weight / 4) + (equipmentWeight / 8));

            return weightResult;
        }
        public double GetItemWeight(string material, string type)
        {
            var materialData = UsagaModel.MaterialCollection.Materials.First(m => m.EnglishName.Equals(material));
            var equipmentTypeData = UsagaModel.EquipmentTypeCollection.EquipmentTypes.First(e => e.EnglishName.Equals(type));

            return materialData.Weight * equipmentTypeData.WeightMultiplier;
        }

    }
}
