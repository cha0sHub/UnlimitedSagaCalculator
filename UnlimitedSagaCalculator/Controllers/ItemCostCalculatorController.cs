using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnlimitedSagaCalculator.Logic;
using UnlimitedSagaCalculator.Model;
using UnlimitedSagaCalculator.Views;

namespace UnlimitedSagaCalculator.Controllers
{
    internal class ItemCostCalculatorController : IItemCostCalculatorController, INotifyPropertyChanged
    {
        private IItemCostCalculatorView View { get; }
        private IUsagaModel UsagaModel { get; }
        private IItemCostCalculator ItemCostCalculator { get; }

        public List<MaterialData> Materials => UsagaModel.MaterialCollection.Materials;
        public List<EquipmentTypeData> EquipmentTypes => UsagaModel.EquipmentTypeCollection.EquipmentTypes;

        private MaterialData _selectedMaterial;
        private EquipmentTypeData _selectedEquipmentType;
        private int _numberOfAbilities;
        private int _durability;
        private string _baseValue;
        private string _equipmentModifier;
        private string _abilityModifier;
        private string _finalCost;
        private string _sellingPrice;

        public MaterialData SelectedMaterial
        {
            get
            {
                return _selectedMaterial;
            }
            set
            {
                _selectedMaterial = value;
                NotifyPropertyChanged();
            }
        }

        public EquipmentTypeData SelectedEquipmentType
        {
            get
            {
                return _selectedEquipmentType;
            }
            set
            {
                _selectedEquipmentType = value;
                NotifyPropertyChanged();
                DetermineFieldVisibility();
            }
        }

        public int NumberOfAbilities
        {
            get
            {
                return _numberOfAbilities;
            }
            set
            {
                if (value <= 0 || value > 4)
                    return;
                _numberOfAbilities = value;
                NotifyPropertyChanged();
            }
        }

        public int Durability
        {
            get 
            {
                return _durability;
            }
            set
            {
                if (value < 0 || value > 100)
                    return;
                _durability = value;
                NotifyPropertyChanged();
            }
        }

        public string BaseValue
        {
            get
            {
                return _baseValue;
            }
            set
            {
                _baseValue = value;
                NotifyPropertyChanged();
            }
        }

        public string EquipmentModifier
        {
            get
            {
                return _equipmentModifier;
            }
            set
            {
                _equipmentModifier = value;
                NotifyPropertyChanged();
            }
        }
        
        public string AbilityModifier
        {
            get
            {
                return _abilityModifier;
            }
            set
            {
                _abilityModifier = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalCost
        {
            get
            {
                return _finalCost;
            }
            set
            {
                _finalCost = value;
                NotifyPropertyChanged();
            }
        }

        public string SellingPrice
        {
            get
            {
                return _sellingPrice;
            }
            set
            {
                _sellingPrice = value;
                NotifyPropertyChanged();
            }
        }

        public ItemCostCalculatorController(IItemCostCalculatorView view, IUsagaModel usagaModel, IItemCostCalculator itemCostCalculator)
        {
            View = view;
            UsagaModel = usagaModel;
            ItemCostCalculator = itemCostCalculator;

            View.SetController(this);
        }

        public void Calculate()
        {
            var itemData = new ItemData
            {
                Material = SelectedMaterial.EnglishName,
                Type = SelectedEquipmentType.EnglishName,
                NumberOfAbilities = NumberOfAbilities,
                Durability = Durability
            };

            var result = ItemCostCalculator.CalculateItemCost(itemData);

            BaseValue = result.BaseValue.ToString("0.00");
            EquipmentModifier = result.EquipmentModifier.ToString("0.00");
            AbilityModifier = result.AbilityModifier.ToString("0.00");
            FinalCost = result.FinalCost.ToString();
            SellingPrice = (result.FinalCost / 2).ToString();
        }

        private void DetermineFieldVisibility()
        {
            if (_selectedEquipmentType == null)
                return;

            if (_selectedEquipmentType.EnglishName == "Head" || _selectedEquipmentType.EnglishName == "Chest" || _selectedEquipmentType.EnglishName == "Leg")
            {
                View.SetNumberOfAbilitiesVisibility(true);
                View.SetDurabilityVisibility(false);
            }
            else if (_selectedEquipmentType.EnglishName == "Material")
            {
                View.SetNumberOfAbilitiesVisibility(false);
                View.SetDurabilityVisibility(false);
                NumberOfAbilities = 1;
            }
            else
            {
                View.SetNumberOfAbilitiesVisibility(true);
                View.SetDurabilityVisibility(true);
            }
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
