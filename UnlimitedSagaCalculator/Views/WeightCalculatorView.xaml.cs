using System.Windows.Controls;
using UnlimitedSagaCalculator.Controllers;

namespace UnlimitedSagaCalculator.Views
{
    /// <summary>
    /// Interaction logic for WeightCalculatorView.xaml
    /// </summary>
    public partial class WeightCalculatorView : Grid, IWeightCalculatorView
    {

        private IWeightCalculatorController Controller { get; set; }

        public WeightCalculatorView()
        {
            InitializeComponent();
        }

        public void SetController(IWeightCalculatorController controller)
        {
            Controller = controller;
            DataContext = controller;
        }

        private void DefaultEquipmentButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.LoadInitialEquipment();
        }

        private void CalculateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.CalculateWeight();
        }

        public void UpdateLanguage()
        {
            CharacterLabel.Content = Properties.Resources.CharacterLabel;
            CharacterColonLabel.Content = Properties.Resources.CharacterColonLabel;
            CharacterWeightLabel.Content = Properties.Resources.CharacterWeightLabel;
            WeaponOneLabel.Content = Properties.Resources.WeaponOneLabel;
            WeaponTwoLabel.Content = Properties.Resources.WeaponTwoLabel;
            AccessoryOneLabel.Content = Properties.Resources.AccessoryOneLabel;
            AccessoryTwoLabel.Content = Properties.Resources.AccessoryTwoLabel;
            HeadLabel.Content = Properties.Resources.HeadLabel;
            ChestLabel.Content = Properties.Resources.ChestLabel;
            LegLabel.Content = Properties.Resources.LegLabel;
            MaterialLabel.Content = Properties.Resources.MaterialLabel;
            WeightLabel.Content = Properties.Resources.WeightNoColonLabel;
            TypeLabel.Content = Properties.Resources.TypeLabel;
            MultiplierLabel.Content = Properties.Resources.MultiplierLabel;

            ResultsLabel.Content = Properties.Resources.ResultsLabel;
            FinalCharacterWeightLabel.Content = Properties.Resources.CharacterWeightLabel;
            FinalWeaponOneWeightLabel.Content = Properties.Resources.WeaponOneWeightLabel;
            FinalWeaponTwoWeightLabel.Content = Properties.Resources.WeaponTwoWeightLabel;
            FinalAccessoryOneWeightLabel.Content = Properties.Resources.AccessoryOneWeightLabel;
            FinalAccessoryTwoWeightLabel.Content = Properties.Resources.AccessoryTwoWeightLabel;
            FinalHeadWeightLabel.Content = Properties.Resources.HeadWeightLabel;
            FinalChestWeightLabel.Content = Properties.Resources.ChestWeightLabel;
            FinalLegWeightLabel.Content = Properties.Resources.LegWeightLabel;
            FinalTotalWeightLabel.Content = Properties.Resources.TotalWeightLabel;
            CalculateButton.Content = Properties.Resources.CalculateLabel;
            DefaultEquipmentButton.Content = Properties.Resources.InitialEquipmentLabel;

            ReloadComboBox(CharacterBox);
            ReloadComboBox(WeaponOneMaterialBox);
            ReloadComboBox(WeaponOneTypeBox);
            ReloadComboBox(WeaponTwoMaterialBox);
            ReloadComboBox(WeaponTwoTypeBox);
            ReloadComboBox(AccessoryOneMaterialBox);
            ReloadComboBox(AccessoryOneTypeBox);
            ReloadComboBox(AccessoryTwoMaterialBox);
            ReloadComboBox(AccessoryTwoTypeBox);
            ReloadComboBox(HeadMaterialBox);
            ReloadComboBox(HeadTypeBox);
            ReloadComboBox(ChestMaterialBox);
            ReloadComboBox(ChestTypeBox);
            ReloadComboBox(LegMaterialBox);
            ReloadComboBox(LegTypeBox);
        }

        private void ReloadComboBox(ComboBox comboBox)
        {
            var oldIndex = comboBox.SelectedIndex;
            comboBox.SelectedIndex = -1;
            comboBox.Items.Refresh();
            comboBox.SelectedIndex = oldIndex;
        }
    }
}
