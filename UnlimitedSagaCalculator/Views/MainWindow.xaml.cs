using System.Windows;
using UnlimitedSagaCalculator.Controllers;

namespace UnlimitedSagaCalculator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {

        private const string EnglishLanguageCode = "en-US";
        private const string JapaneseLanguageCode = "ja-JP";

        private IMainController Controller { get; set; }
        private IDamageCalculatorView DamageCalculatorView { get; }
        private ICharacterStatCalculatorView CharacterStatCalculatorView { get; }
        private IWeightCalculatorView WeightCalculatorView { get; }
        private IItemCostCalculatorView ItemCostCalculatorView { get; }

        public MainWindow(IDamageCalculatorView damageCalculatorView, ICharacterStatCalculatorView characterStatCalculatorView, IWeightCalculatorView weightCalculatorView, IItemCostCalculatorView itemCostCalculatorView)
        {
            InitializeComponent();
            ContentPlaceholder.Content = damageCalculatorView;
            DamageCalculatorView = damageCalculatorView;
            CharacterStatCalculatorView = characterStatCalculatorView;
            WeightCalculatorView = weightCalculatorView;
            ItemCostCalculatorView = itemCostCalculatorView;
        }

        public void SetController(IMainController controller)
        {
            Controller = controller;
            DataContext = controller;
        }

        public void ShowWindow()
        {
            ShowDialog();
        }

        private void EnglishLanguageItem_Click(object sender, RoutedEventArgs e)
        {
            Controller.SetLanguage(EnglishLanguageCode);
        }

        private void JapaneseLanguageItem_Click(object sender, RoutedEventArgs e)
        {
            Controller.SetLanguage(JapaneseLanguageCode);
        }

        private void DamageCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            ContentPlaceholder.Content = DamageCalculatorView;
        }

        private void CharacterStatCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            ContentPlaceholder.Content = CharacterStatCalculatorView;
        }

        private void WeightCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            ContentPlaceholder.Content = WeightCalculatorView;
        }

        private void ItemCostCalculatorButton_Click(object sender, RoutedEventArgs e)
        {
            ContentPlaceholder.Content = ItemCostCalculatorView;
        }

        public void DisplayDamageCalculatorView()
        {
            ContentPlaceholder.Content = DamageCalculatorView;
        }

        public void UpdateLanguage()
        {
            DamageCalculatorButton.Content = Properties.Resources.DamageCalculatorLabel;
            CharacterStatCalculatorButton.Content = Properties.Resources.CharacterStatCalculatorLabel;
            WeightCalculatorButton.Content = Properties.Resources.WeightCalculatorLabel;
            ItemCostCalculatorButton.Content = Properties.Resources.ItemCostCalculatorLabel;
        }
    }
}