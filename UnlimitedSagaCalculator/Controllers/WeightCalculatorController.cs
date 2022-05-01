using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnlimitedSagaCalculator.Logic;
using UnlimitedSagaCalculator.Model;
using UnlimitedSagaCalculator.Views;

namespace UnlimitedSagaCalculator.Controllers
{
    internal class WeightCalculatorController : IWeightCalculatorController, INotifyPropertyChanged
    {

        private IWeightCalculatorView View { get; }
        private IWeightCalculator WeightCalculator { get; }
        private IUsagaModel UsagaModel { get; }

        private CharacterData _selectedCharacter;
        private string _characterWeight;
        private MaterialData _selectedWeaponOneMaterial;
        private MaterialData _selectedWeaponTwoMaterial;
        private MaterialData _selectedAccessoryOneMaterial;
        private MaterialData _selectedAccessoryTwoMaterial;
        private MaterialData _selectedHeadMaterial;
        private MaterialData _selectedChestMaterial;
        private MaterialData _selectedLegMaterial;
        private string _weaponOneMaterialWeight;
        private string _weaponTwoMaterialWeight;
        private string _accessoryOneMaterialWeight;
        private string _accessoryTwoMaterialWeight;
        private string _headMaterialWeight;
        private string _chestMaterialWeight;
        private string _legMaterialWeight;
        private EquipmentTypeData _selectedWeaponOneType;
        private EquipmentTypeData _selectedWeaponTwoType;
        private EquipmentTypeData _selectedAccessoryOneType;
        private EquipmentTypeData _selectedAccessoryTwoType;
        private EquipmentTypeData _selectedHeadType;
        private EquipmentTypeData _selectedChestType;
        private EquipmentTypeData _selectedLegType;
        private string _weaponOneTypeMultiplier;
        private string _weaponTwoTypeMultiplier;
        private string _accessoryOneTypeMultiplier;
        private string _accessoryTwoTypeMultiplier;
        private string _headTypeMultiplier;
        private string _chestTypeMultiplier;
        private string _legTypeMultiplier;
        private string _finalCharacterWeight;
        private string _finalWeaponOneWeight;
        private string _finalWeaponTwoWeight;
        private string _finalAccessoryOneWeight;
        private string _finalAccessoryTwoWeight;
        private string _finalHeadWeight;
        private string _finalChestWeight;
        private string _finalLegWeight;
        private string _finalTotalWeight;

        public List<CharacterData> Characters => UsagaModel.CharacterCollection.Characters;
        public List<MaterialData> Materials => UsagaModel.MaterialCollection.Materials;
        public List<EquipmentTypeData> EquipmentTypes => UsagaModel.EquipmentTypeCollection.EquipmentTypes;

        public CharacterData SelectedCharacter
        {
            get
            {
                return _selectedCharacter;
            }
            set
            {
                _selectedCharacter = value;
                NotifyPropertyChanged();
                CharacterWeight = _selectedCharacter?.Weight.ToString("0");
            }
        }

        public string CharacterWeight
        {
            get
            {
                return _characterWeight;
            }
            set
            {
                _characterWeight = value;
                NotifyPropertyChanged();
            }
        }

        public MaterialData SelectedWeaponOneMaterial
        {
            get
            {
                return _selectedWeaponOneMaterial;
            }
            set
            {
                _selectedWeaponOneMaterial = value;
                NotifyPropertyChanged();
                WeaponOneMaterialWeight = value?.Weight.ToString();
            }
        }

        public MaterialData SelectedWeaponTwoMaterial
        {
            get
            {
                return _selectedWeaponTwoMaterial;
            }
            set
            {
                _selectedWeaponTwoMaterial = value;
                NotifyPropertyChanged();
                WeaponTwoMaterialWeight = value?.Weight.ToString();
            }
        }

        public MaterialData SelectedAccessoryOneMaterial
        {
            get
            {
                return _selectedAccessoryOneMaterial;
            }
            set
            {
                _selectedAccessoryOneMaterial = value;
                NotifyPropertyChanged();
                AccessoryOneMaterialWeight = value?.Weight.ToString();
            }
        }

        public MaterialData SelectedAccessoryTwoMaterial
        {
            get
            {
                return _selectedAccessoryTwoMaterial;
            }
            set
            {
                _selectedAccessoryTwoMaterial = value;
                NotifyPropertyChanged();
                AccessoryTwoMaterialWeight = value?.Weight.ToString();
            }
        }

        public MaterialData SelectedHeadMaterial
        {
            get
            {
                return _selectedHeadMaterial;
            }
            set
            {
                _selectedHeadMaterial = value;
                NotifyPropertyChanged();
                HeadMaterialWeight = value?.Weight.ToString();
            }
        }

        public MaterialData SelectedChestMaterial
        {
            get
            {
                return _selectedChestMaterial;
            }
            set
            {
                _selectedChestMaterial = value;
                NotifyPropertyChanged();
                ChestMaterialWeight = value?.Weight.ToString();
            }
        }

        public MaterialData SelectedLegMaterial
        {
            get
            {
                return _selectedLegMaterial;
            }
            set
            {
                _selectedLegMaterial = value;
                NotifyPropertyChanged();
                LegMaterialWeight = value?.Weight.ToString();
            }
        }

        public string WeaponOneMaterialWeight
        {
            get
            {
                return _weaponOneMaterialWeight;
            }
            set
            {
                _weaponOneMaterialWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string WeaponTwoMaterialWeight
        {
            get
            {
                return _weaponTwoMaterialWeight;
            }
            set
            {
                _weaponTwoMaterialWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string AccessoryOneMaterialWeight
        {
            get
            {
                return _accessoryOneMaterialWeight;
            }
            set
            {
                _accessoryOneMaterialWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string AccessoryTwoMaterialWeight
        {
            get
            {
                return _accessoryTwoMaterialWeight;
            }
            set
            {
                _accessoryTwoMaterialWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string HeadMaterialWeight
        {
            get
            {
                return _headMaterialWeight;
            }
            set
            {
                _headMaterialWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string ChestMaterialWeight
        {
            get
            {
                return _chestMaterialWeight;
            }
            set
            {
                _chestMaterialWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string LegMaterialWeight
        {
            get
            {
                return _legMaterialWeight;
            }
            set
            {
                _legMaterialWeight = value;
                NotifyPropertyChanged();
            }
        }

        public EquipmentTypeData SelectedWeaponOneType
        {
            get
            {
                return _selectedWeaponOneType;
            }
            set
            {
                _selectedWeaponOneType = value;
                NotifyPropertyChanged();
                WeaponOneTypeMultiplier = value?.WeightMultiplier.ToString();
            }
        }

        public EquipmentTypeData SelectedWeaponTwoType
        {
            get
            {
                return _selectedWeaponTwoType;
            }
            set
            {
                _selectedWeaponTwoType = value;
                NotifyPropertyChanged();
                WeaponTwoTypeMultiplier = value?.WeightMultiplier.ToString();
            }
        }

        public EquipmentTypeData SelectedAccessoryOneType
        {
            get
            {
                return _selectedAccessoryOneType;
            }
            set
            {
                _selectedAccessoryOneType = value;
                NotifyPropertyChanged();
                AccessoryOneTypeMultiplier = value?.WeightMultiplier.ToString();
            }
        }

        public EquipmentTypeData SelectedAccessoryTwoType
        {
            get
            {
                return _selectedAccessoryTwoType;
            }
            set
            {
                _selectedAccessoryTwoType = value;
                NotifyPropertyChanged();
                AccessoryTwoTypeMultiplier = value?.WeightMultiplier.ToString();
            }
        }

        public EquipmentTypeData SelectedHeadType
        {
            get
            {
                return _selectedHeadType;
            }
            set
            {
                _selectedHeadType = value;
                NotifyPropertyChanged();
                HeadTypeMultiplier = value?.WeightMultiplier.ToString();
            }
        }

        public EquipmentTypeData SelectedChestType
        {
            get
            {
                return _selectedChestType;
            }
            set
            {
                _selectedChestType = value;
                NotifyPropertyChanged();
                ChestTypeMultiplier = value?.WeightMultiplier.ToString();
            }
        }

        public EquipmentTypeData SelectedLegType
        {
            get
            {
                return _selectedLegType;
            }
            set
            {
                _selectedLegType = value;
                NotifyPropertyChanged();
                LegTypeMultiplier = value?.WeightMultiplier.ToString();
            }
        }

        public string WeaponOneTypeMultiplier
        {
            get
            {
                return _weaponOneTypeMultiplier;
            }
            set
            {
                _weaponOneTypeMultiplier = value;
                NotifyPropertyChanged();
            }
        }

        public string WeaponTwoTypeMultiplier
        {
            get
            {
                return _weaponTwoTypeMultiplier;
            }
            set
            {
                _weaponTwoTypeMultiplier = value;
                NotifyPropertyChanged();
            }
        }

        public string AccessoryOneTypeMultiplier
        {
            get
            {
                return _accessoryOneTypeMultiplier;
            }
            set
            {
                _accessoryOneTypeMultiplier = value;
                NotifyPropertyChanged();
            }
        }

        public string AccessoryTwoTypeMultiplier
        {
            get
            {
                return _accessoryTwoTypeMultiplier;
            }
            set
            {
                _accessoryTwoTypeMultiplier = value;
                NotifyPropertyChanged();
            }
        }

        public string HeadTypeMultiplier
        {
            get
            {
                return _headTypeMultiplier;
            }
            set
            {
                _headTypeMultiplier = value;
                NotifyPropertyChanged();
            }
        }

        public string ChestTypeMultiplier
        {
            get
            {
                return _chestTypeMultiplier;
            }
            set
            {
                _chestTypeMultiplier = value;
                NotifyPropertyChanged();
            }
        }

        public string LegTypeMultiplier
        {
            get
            {
                return _legTypeMultiplier;
            }
            set
            {
                _legTypeMultiplier = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalCharacterWeight
        {
            get
            {
                return _finalCharacterWeight;
            }
            set
            {
                _finalCharacterWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalWeaponOneWeight
        {
            get
            {
                return _finalWeaponOneWeight;
            }
            set
            {
                _finalWeaponOneWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalWeaponTwoWeight
        {
            get
            {
                return _finalWeaponTwoWeight;
            }
            set
            {
                _finalWeaponTwoWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalAccessoryOneWeight
        {
            get
            {
                return _finalAccessoryOneWeight;
            }
            set
            {
                _finalAccessoryOneWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalAccessoryTwoWeight
        {
            get
            {
                return _finalAccessoryTwoWeight;
            }
            set
            {
                _finalAccessoryTwoWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalHeadWeight
        {
            get
            {
                return _finalHeadWeight;
            }
            set
            {
                _finalHeadWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalChestWeight
        {
            get
            {
                return _finalChestWeight;
            }
            set
            {
                _finalChestWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalLegWeight
        {
            get
            {
                return _finalLegWeight;
            }
            set
            {
                _finalLegWeight = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalTotalWeight
        {
            get
            {
                return _finalTotalWeight;
            }
            set
            {
                _finalTotalWeight = value;
                NotifyPropertyChanged();
            }
        }

        public WeightCalculatorController(IWeightCalculatorView view, IWeightCalculator weightCalculator, IUsagaModel usagaModel)
        {
            View = view;
            WeightCalculator = weightCalculator;
            UsagaModel = usagaModel;

            View.SetController(this);
        }

        public void LoadInitialEquipment()
        {
            if (SelectedCharacter == null)
                return;

            if (!string.IsNullOrEmpty(SelectedCharacter.WeaponOneMaterial))
            {
                SelectedWeaponOneMaterial = Materials.First(m => m.EnglishName == SelectedCharacter.WeaponOneMaterial);
                SelectedWeaponOneType = EquipmentTypes.First(e => e.EnglishName == SelectedCharacter.WeaponOneType);
            }
            if (!string.IsNullOrEmpty(SelectedCharacter.WeaponTwoMaterial))
            {
                SelectedWeaponTwoMaterial = Materials.First(m => m.EnglishName == SelectedCharacter.WeaponTwoMaterial);
                SelectedWeaponTwoType = EquipmentTypes.First(e => e.EnglishName == SelectedCharacter.WeaponTwoType);
            }
            if (!string.IsNullOrEmpty(SelectedCharacter.AccessoryOneMaterial))
            {
                SelectedAccessoryOneMaterial = Materials.First(m => m.EnglishName == SelectedCharacter.AccessoryOneMaterial);
                SelectedAccessoryOneType = EquipmentTypes.First(e => e.EnglishName == "Accessory");
            }
            if (!string.IsNullOrEmpty(SelectedCharacter.AccessoryTwoMaterial))
            {
                SelectedAccessoryTwoMaterial = Materials.First(m => m.EnglishName == SelectedCharacter.AccessoryTwoMaterial);
                SelectedAccessoryTwoType = EquipmentTypes.First(e => e.EnglishName == "Accessory");
            }
            if (!string.IsNullOrEmpty(SelectedCharacter.HeadMaterial))
            {
                SelectedHeadMaterial = Materials.First(m => m.EnglishName == SelectedCharacter.HeadMaterial);
                SelectedHeadType = EquipmentTypes.First(e => e.EnglishName == "Head");
            }
            if (!string.IsNullOrEmpty(SelectedCharacter.ChestMaterial))
            {
                SelectedChestMaterial = Materials.First(m => m.EnglishName == SelectedCharacter.ChestMaterial);
                SelectedChestType = EquipmentTypes.First(e => e.EnglishName == "Chest");
            }
            if (!string.IsNullOrEmpty(SelectedCharacter.LegsMaterial))
            {
                SelectedLegMaterial = Materials.First(m => m.EnglishName == SelectedCharacter.LegsMaterial);
                SelectedLegType = EquipmentTypes.First(e => e.EnglishName == "Leg");
            }
        }

        public void CalculateWeight()
        {
            var characterData = new CharacterData();

            characterData.Weight = SelectedCharacter.Weight;
            if(SelectedWeaponOneMaterial != null)
                characterData.WeaponOneMaterial = SelectedWeaponOneMaterial.EnglishName;
            if(SelectedWeaponOneType != null)
                characterData.WeaponOneType = SelectedWeaponOneType.EnglishName;
            if(SelectedWeaponTwoMaterial != null)
                characterData.WeaponTwoMaterial = SelectedWeaponTwoMaterial.EnglishName;
            if(SelectedWeaponTwoType != null)
                characterData.WeaponTwoType = SelectedWeaponTwoType.EnglishName;
            if(SelectedAccessoryOneMaterial != null)
                characterData.AccessoryOneMaterial = SelectedAccessoryOneMaterial.EnglishName;
            if(SelectedAccessoryOneType != null)
                characterData.AccessoryOneType = SelectedAccessoryOneType.EnglishName;
            if(SelectedAccessoryTwoMaterial != null)
                characterData.AccessoryTwoMaterial = SelectedAccessoryTwoMaterial.EnglishName;
            if(SelectedAccessoryTwoType != null)
                characterData.AccessoryTwoType = SelectedAccessoryTwoType.EnglishName;
            if(SelectedHeadMaterial != null)
                characterData.HeadMaterial = SelectedHeadMaterial.EnglishName;
            if(SelectedHeadType != null)
                characterData.HeadType = SelectedHeadType.EnglishName;
            if(SelectedChestMaterial != null)
                characterData.ChestMaterial = SelectedChestMaterial.EnglishName;
            if(SelectedChestType != null)
                characterData.ChestType = SelectedChestType.EnglishName;
            if(SelectedLegMaterial != null)
                characterData.LegsMaterial = SelectedLegMaterial.EnglishName;
            if(SelectedLegType != null)
                characterData.LegsType = SelectedLegType.EnglishName;
            
            var weightResult = WeightCalculator.CalculateWeight(characterData);

            FinalCharacterWeight = weightResult.CharacterWeight.ToString("0.00");
            FinalWeaponOneWeight = weightResult.WeaponOneWeight.ToString("0.00");
            FinalWeaponTwoWeight = weightResult.WeaponTwoWeight.ToString("0.00");
            FinalAccessoryOneWeight = weightResult.AccessoryOneWeight.ToString("0.00");
            FinalAccessoryTwoWeight = weightResult.AccessoryTwoWeight.ToString("0.00");
            FinalHeadWeight = weightResult.HeadWeight.ToString("0.00");
            FinalChestWeight = weightResult.ChestWeight.ToString("0.00");
            FinalLegWeight = weightResult.LegWeight.ToString("0.00");
            FinalTotalWeight = weightResult.TotalWeight.ToString();
        }

        public void UpdateLanguage()
        {
            View.UpdateLanguage();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
