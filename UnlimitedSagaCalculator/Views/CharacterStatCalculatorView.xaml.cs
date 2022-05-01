using System.Windows.Controls;
using UnlimitedSagaCalculator.Controllers;

namespace UnlimitedSagaCalculator.Views
{
    /// <summary>
    /// Interaction logic for CharacterStatCalculatorView.xaml
    /// </summary>
    public partial class CharacterStatCalculatorView : Grid, ICharacterStatCalculatorView
    {
        private ICharacterStatCalculatorController Controller { get; set; }

        public CharacterStatCalculatorView()
        {
            InitializeComponent();
        }

        public void SetController(ICharacterStatCalculatorController controller)
        {
            Controller = controller;
            DataContext = controller;
        }

        public void UpdateLanguage()
        {
            CharacterLabel.Content = Properties.Resources.CharacterLabel;
            CharacterStrengthLabel.Content = Properties.Resources.StrengthLabel;
            CharacterSkillLabel.Content = Properties.Resources.SkillLabel;
            CharacterSpiritLabel.Content = Properties.Resources.SpiritLabel;
            CharacterMagicLabel.Content = Properties.Resources.MagicLabel;
            CharacterEnduranceLabel.Content = Properties.Resources.EnduranceLabel;
            CharacterFireLabel.Content = Properties.Resources.FireLabel;
            CharacterEarthLabel.Content = Properties.Resources.EarthLabel;
            CharacterMetalLabel.Content = Properties.Resources.MetalLabel;
            CharacterWaterLabel.Content = Properties.Resources.WaterLabel;
            CharacterWoodLabel.Content = Properties.Resources.WoodLabel;
            CharacterWeightLabel.Content = Properties.Resources.WeightLabel;
            RawLabel.Content = Properties.Resources.RawLabel;
            BonusLabel.Content = Properties.Resources.BonusLabel;
            FinalLabel.Content = Properties.Resources.FinalLabel;
            ResultStrengthLabel.Content = Properties.Resources.StrengthLabel;
            ResultSkillLabel.Content = Properties.Resources.SkillLabel;
            ResultSpiritLabel.Content = Properties.Resources.SpiritLabel;
            ResultMagicLabel.Content = Properties.Resources.MagicLabel;
            ResultEnduranceLabel.Content = Properties.Resources.EnduranceLabel;
            ResultFireLabel.Content = Properties.Resources.FireLabel;
            ResultEarthLabel.Content = Properties.Resources.EarthLabel;
            ResultMetalLabel.Content = Properties.Resources.MetalLabel;
            ResultWaterLabel.Content = Properties.Resources.WaterLabel;
            ResultWoodLabel.Content = Properties.Resources.WoodLabel;
            StartingPanelsButton.Content = Properties.Resources.StartingPanelsLabel;
            CalculateButton.Content = Properties.Resources.CalculateLabel;
            TransferButton.Content = Properties.Resources.TransferLabel;

            ReloadComboBox(CharacterBox);
            ReloadComboBox(NwPanelTypeBox);
            ReloadComboBox(NwPanelBox);
            ReloadComboBox(NePanelTypeBox);
            ReloadComboBox(NePanelBox);
            ReloadComboBox(WPanelTypeBox);
            ReloadComboBox(WPanelBox);
            ReloadComboBox(CPanelTypeBox);
            ReloadComboBox(CPanelBox);
            ReloadComboBox(EPanelTypeBox);
            ReloadComboBox(EPanelBox);
            ReloadComboBox(SwPanelTypeBox);
            ReloadComboBox(SwPanelBox);
            ReloadComboBox(SePanelTypeBox);
            ReloadComboBox(SePanelBox);
        }

        private void ReloadComboBox(ComboBox comboBox)
        {
            var oldIndex = comboBox.SelectedIndex;
            comboBox.SelectedIndex = -1;
            comboBox.Items.Refresh();
            comboBox.SelectedIndex = oldIndex;
        }

        private void StartingPanelsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.SetStartingPanels();
        }

        private void CalculateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.Calculate();
        }

        private void TransferButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.Transfer();
        }
    }
}
