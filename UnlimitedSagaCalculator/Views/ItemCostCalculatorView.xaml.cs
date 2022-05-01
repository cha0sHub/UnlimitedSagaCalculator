using System.Windows.Controls;
using UnlimitedSagaCalculator.Controllers;

namespace UnlimitedSagaCalculator.Views
{
    /// <summary>
    /// Interaction logic for ItemCostCalculatorView.xaml
    /// </summary>
    public partial class ItemCostCalculatorView : Grid, IItemCostCalculatorView
    {
        private IItemCostCalculatorController Controller { get; set; }

        public ItemCostCalculatorView()
        {
            InitializeComponent();
        }

        public void SetController(IItemCostCalculatorController controller)
        {
            Controller = controller;
            DataContext = controller;
        }

        private void CalculateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.Calculate();
        }

        public void SetNumberOfAbilitiesVisibility(bool isVisible)
        {
            if (isVisible)
            {
                NumberOfAbilitiesLabel.Visibility = System.Windows.Visibility.Visible;
                NumberOfAbilitiesBox.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                NumberOfAbilitiesLabel.Visibility = System.Windows.Visibility.Collapsed;
                NumberOfAbilitiesBox.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        public void SetDurabilityVisibility(bool isVisible)
        {
            if (isVisible)
            {
                DurabilityLabel.Visibility = System.Windows.Visibility.Visible;
                DurabilityBox.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                DurabilityLabel.Visibility = System.Windows.Visibility.Collapsed;
                DurabilityBox.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        public void UpdateLanguage()
        {
            ItemLabel.Content = Properties.Resources.ItemLabel;
            MaterialLabel.Content = Properties.Resources.MaterialWithColonLabel;
            TypeLabel.Content = Properties.Resources.TypeWithColonLabel;
            NumberOfAbilitiesLabel.Content = Properties.Resources.NumberOfAbilitiesLabel;
            DurabilityLabel.Content = Properties.Resources.DurabilityLabel;
            BaseValueLabel.Content = Properties.Resources.BaseValueLabel;
            EquipmentModifierLabel.Content = Properties.Resources.EquipmentModifierLabel;
            AbilityModifierLabel.Content = Properties.Resources.AbilityModifierLabel;
            FinalCostLabel.Content = Properties.Resources.FinalCostLabel;
            SellingPriceLabel.Content = Properties.Resources.SellingPriceLabel;
            CalculateButton.Content = Properties.Resources.CalculateLabel;

            ReloadComboBox(MaterialBox);
            ReloadComboBox(TypeBox);
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
