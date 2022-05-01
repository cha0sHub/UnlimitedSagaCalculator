using System.Windows;
using System.Windows.Controls;
using UnlimitedSagaCalculator.Controllers;
using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Views
{
    /// <summary>
    /// Interaction logic for DamageCalculatorView.xaml
    /// </summary>
    public partial class DamageCalculatorView : Grid, IDamageCalculatorView
    {
        private IDamageCalculatorController Controller { get; set; }

        public DamageCalculatorView()
        {
            InitializeComponent();
        }

        public void SetController(IDamageCalculatorController controller)
        {
            Controller = controller;
            DataContext = controller;
        }

        private void CalculateHpDamageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.CalculateHpDamage();
        }

        private void CalculateLpDamageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.CalculateLpDamage();
        }

        public void UpdateLanguage()
        {
            AttackerLabel.Content = Properties.Resources.AttackerLabel;
            AttackerCurrentHpLabel.Content = Properties.Resources.CurrentHpLabel;
            AttackerMaxHpLabel.Content = Properties.Resources.MaxHpLabel;
            AttackerStrengthLabel.Content = Properties.Resources.StrengthLabel;
            AttackerSkillLabel.Content = Properties.Resources.SkillLabel;
            AttackerWeightLabel.Content = Properties.Resources.WeightLabel;
            AttackerWeaponMaterialLabel.Content = Properties.Resources.WeaponMaterialLabel;
            AttackerWeaponPowerLabel.Content = Properties.Resources.WeaponPowerLabel;
            AttackerFireLabel.Content = Properties.Resources.FireLabel;
            AttackerWaterLabel.Content = Properties.Resources.WaterLabel;
            AttackerWoodLabel.Content = Properties.Resources.WoodLabel;
            AttackerEarthLabel.Content = Properties.Resources.EarthLabel;
            AttackerMetalLabel.Content = Properties.Resources.MetalLabel;

            EnemyColonLabel.Content = Properties.Resources.EnemyColonLabel;
            EnemyLabel.Content = Properties.Resources.EnemyLabel;
            EnemyCurrentHpLabel.Content = Properties.Resources.CurrentHpLabel;
            EnemyMaxHpLabel.Content = Properties.Resources.MaxHpLabel;
            SlashDefenseLabel.Content = Properties.Resources.SlashDefenseLabel;
            HitDefenseLabel.Content = Properties.Resources.HitDefenseLabel;
            PierceDefenseLabel.Content = Properties.Resources.PierceDefenseLabel;
            HeatDefenseLabel.Content = Properties.Resources.HeatDefenseLabel;
            ColdDefenseLabel.Content = Properties.Resources.ColdDefenseLabel;
            LightningDefenseLabel.Content = Properties.Resources.LightningDefenseLabel;
            LightDefenseLabel.Content = Properties.Resources.LightDefenseLabel;
            LpDefenseLabel.Content = Properties.Resources.LpDefenseLabel;

            AttackColonLabel.Content = Properties.Resources.AttackColonLabel;
            AttackLabel.Content = Properties.Resources.AttackLabel;
            AttackTypeLabel.Content = Properties.Resources.AttackTypeLabel;
            WeaponTypeLabel.Content = Properties.Resources.WeaponTypeLabel;
            MagicTypeLabel.Content = Properties.Resources.MagicTypeLabel;
            EnCostLabel.Content = Properties.Resources.EnCostLabel;
            GrowthRateLabel.Content = Properties.Resources.GrowthRateLabel;
            HpDamageLabel.Content = Properties.Resources.HpDamageLabel;
            LpDamageLabel.Content = Properties.Resources.LpDamageLabel;
            NumberOfHitsLabel.Content = Properties.Resources.NumberOfHitsLabel;
            AttributesLabel.Content = Properties.Resources.AttributesLabel;
            ComboPercentageLabel.Content = Properties.Resources.ComboPercentageLabel;
            AttackDamageLabel.Content = Properties.Resources.AttackDamageLabel;
            ComboDamageLabel.Content = Properties.Resources.ComboDamageLabel;

            HpDamageCalculationLabel.Content = Properties.Resources.HpDamageCalculation;
            RawDamageLabel.Content = Properties.Resources.RawDamageLabel;
            FinalDamageLabel.Content = Properties.Resources.FinalDamageLabel;
            CalculateHpDamageButton.Content = Properties.Resources.CalculateLabel;

            LpDamageCalculationLabel.Content = Properties.Resources.LpDamageCalculation;
            DamageProbabilityLabel.Content = Properties.Resources.DamageProbabilityLabel;
            ZeroDamageLabel.Content = Properties.Resources.ZeroDamageLabel;
            OneDamageLabel.Content = Properties.Resources.OneDamageLabel;
            TwoDamageLabel.Content = Properties.Resources.TwoDamageLabel;
            ThreeDamageLabel.Content = Properties.Resources.ThreeDamageLabel;
            CalculateLpDamageButton.Content = Properties.Resources.CalculateLabel;
            SetHpDamageButton.Content = Properties.Resources.SetDamageLabel;
            AddToComboButton.Content = Properties.Resources.AddToComboLabel;

            ReloadComboBox(EnemyBox);
            ReloadComboBox(AttackBox);
            ReloadComboBox(EnemyCategoryBox);
            ReloadComboBox(WeaponTypeBox);
            ReloadComboBox(AttackerWeaponMaterialBox);
        }

        private void ReloadComboBox(ComboBox comboBox)
        {
            var oldIndex = comboBox.SelectedIndex;
            comboBox.SelectedIndex = -1;
            comboBox.Items.Refresh();
            comboBox.SelectedIndex = oldIndex;
        }

        private void SetHpDamageButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.SetDamage();
        }

        private void AddToComboButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Controller.AddToCombo();
        }

        public void BoldMartialFields()
        {
            UnboldAllFields();
            AttackerStrengthLabel.FontWeight = FontWeights.Bold;
            AttackerSkillLabel.FontWeight = FontWeights.Bold;
            AttackerWeightLabel.FontWeight = FontWeights.Bold;
        }

        public void BoldStrengthFields()
        {
            UnboldAllFields();
            AttackerStrengthLabel.FontWeight = FontWeights.Bold;
            AttackerWeaponMaterialLabel.FontWeight = FontWeights.Bold;
            AttackerWeaponPowerLabel.FontWeight = FontWeights.Bold;
        }

        public void BoldSkillFields()
        {
            UnboldAllFields();
            AttackerSkillLabel.FontWeight = FontWeights.Bold;
            AttackerWeaponMaterialLabel.FontWeight = FontWeights.Bold;
            AttackerWeaponPowerLabel.FontWeight = FontWeights.Bold;
        }

        public void BoldMagicFields(MagicType magicType)
        {
            UnboldAllFields();
            AttackerMagicLabel.FontWeight = FontWeights.Bold;
            switch (magicType)
            {
                case MagicType.Fire:
                    AttackerFireLabel.FontWeight = FontWeights.Bold;
                    break;
                case MagicType.Water:
                    AttackerWaterLabel.FontWeight = FontWeights.Bold;
                    break;
                case MagicType.Wood:
                    AttackerWoodLabel.FontWeight = FontWeights.Bold;
                    break;
                case MagicType.Earth:
                    AttackerEarthLabel.FontWeight = FontWeights.Bold;
                    break;
                case MagicType.Metal:
                    AttackerMetalLabel.FontWeight = FontWeights.Bold;
                    break;
            }
        }

        private void UnboldAllFields()
        {
            AttackerStrengthLabel.FontWeight = FontWeights.Normal;
            AttackerSkillLabel.FontWeight = FontWeights.Normal;
            AttackerMagicLabel.FontWeight = FontWeights.Normal;
            AttackerWeightLabel.FontWeight = FontWeights.Normal;
            AttackerWeaponMaterialLabel.FontWeight = FontWeights.Normal;
            AttackerWeaponPowerLabel.FontWeight = FontWeights.Normal;
            AttackerFireLabel.FontWeight = FontWeights.Normal;
            AttackerWaterLabel.FontWeight = FontWeights.Normal;
            AttackerWoodLabel.FontWeight = FontWeights.Normal;
            AttackerEarthLabel.FontWeight = FontWeights.Normal;
            AttackerMetalLabel.FontWeight = FontWeights.Normal;
        }
    }
}
