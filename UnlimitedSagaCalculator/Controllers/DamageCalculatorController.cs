using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using UnlimitedSagaCalculator.Logic;
using UnlimitedSagaCalculator.Model;
using UnlimitedSagaCalculator.Views;

namespace UnlimitedSagaCalculator.Controllers
{
    internal class DamageCalculatorController : IDamageCalculatorController, INotifyPropertyChanged
    {
        private IDamageCalculatorView View { get; set; }
        private IUsagaModel UsagaModel { get; set; }
        private IHpDamageCalculator HpDamageCalculator { get; }
        private ILpDamageCalculator LpDamageCalculator { get; }

        public Dictionary<EnemyCategory,string> EnemyCategories { get; } = new Dictionary<EnemyCategory, string>();
        public Dictionary<WeaponType, string> WeaponTypes { get; } = new Dictionary<WeaponType, string>();

        public ObservableCollection<EnemyData> Enemies { get; } = new ObservableCollection<EnemyData>();
        public ObservableCollection<AttackData> Attacks { get; } = new ObservableCollection<AttackData>();
        public ObservableCollection<MaterialData> WeaponMaterials { get; } = new ObservableCollection<MaterialData>();
        private EnemyData _selectedEnemy;
        private AttackData _selectedAttack;

        private int _attackerCurrentHp;
        private int _attackerMaxHp = 1;
        private int _attackerSkill;
        private int _attackerStrength;
        private int _attackerWeaponPower;
        private int _attackerWeight;
        private int _attackerMagic;
        private int _attackerFire;
        private int _attackerWater;
        private int _attackerWood;
        private int _attackerEarth;
        private int _attackerMetal;
        private MaterialData _selectedWeaponMaterial;

        private int _enemyCurrentHp;
        private int _enemyMaxHp;
        private int _enemyLp;
        private int _enemySlashDefense;
        private int _enemyHitDefense;
        private int _enemyPierceDefense;
        private int _enemyHeatDefense;
        private int _enemyColdDefense;
        private int _enemyLightningDefense;
        private int _enemyLightDefense;
        private int _enemyLpDefense;

        private AttackType _attackType;
        private WeaponType _weaponType;
        private MagicType _magicType;
        private int _enCost;
        private int _growthRate;
        private int _hpDamage;
        private int _lpDamage;
        private int _numberOfHits;
        private AttackAttribute[] _attackAttributes;
        private string _attackAttributesString;
        private string _rawDamageString;
        private string _finalDamageString;
        private int _finalDamage;
        private double _comboPercentage = 1.00;
        private string _damageProbability;
        private string _oddsOfNoDamage;
        private string _oddsOfOneDamage;
        private string _oddsOfTwoDamage;
        private string _oddsOfThreeDamage;
        private int _attackDamage;
        private string _attackTypeString;
        private string _weaponTypeString;
        private string _magicTypeString;
        private WeaponType _selectedWeaponTypeFilter;
        private EnemyCategory _selectedEnemyCategoryFilter;
        private int _comboDamage;

        public int AttackerCurrentHp
        {
            get
            {
                return _attackerCurrentHp;
            }
            set
            {
                if (value < 0)
                    return;
                _attackerCurrentHp = value;
                NotifyPropertyChanged();
            }

        }

        public int AttackerMaxHp
        {
            get
            {
                return _attackerMaxHp;
            }
            set
            {
                if (value < 1)
                    return;
                _attackerMaxHp = value;
                NotifyPropertyChanged();
            }
        }

        public int AttackerSkill
        {
            get
            {
                return _attackerSkill;
            }
            set
            {
                if (value < 1 || value > 255)
                    return;
                _attackerSkill = value;
                NotifyPropertyChanged();
            }
        }

        public int AttackerStrength
        {
            get
            {
                return _attackerStrength;
            }
            set
            {
                if (value < 1 || value > 255)
                    return;
                _attackerStrength = value;
                NotifyPropertyChanged();
            }
        }

        public int AttackerWeaponPower
        {
            get
            {
                return _attackerWeaponPower;
            }
            set
            {
                if (value < 1 || value > 255)
                    return;
                _attackerWeaponPower = value;
                NotifyPropertyChanged();
            }
        }

        public int AttackerWeight
        {
            get
            {
                return _attackerWeight;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _attackerWeight = value;
                NotifyPropertyChanged();
            }
        }

        public int AttackerMagic
        {
            get
            {
                return _attackerMagic;
            }
            set
            {
                if (value < 1 || value > 255)
                    return;
                _attackerMagic = value;
                NotifyPropertyChanged();
            }
        }

        public int AttackerFire
        {
            get
            {
                return _attackerFire;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _attackerFire = value;
                NotifyPropertyChanged();
            }
        }

        public int AttackerWater
        {
            get
            {
                return _attackerWater;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _attackerWater = value;
                NotifyPropertyChanged();
            }
        }
        public int AttackerWood
        {
            get
            {
                return _attackerWood;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _attackerWood = value;
                NotifyPropertyChanged();
            }
        }

        public int AttackerEarth
        {
            get
            {
                return _attackerEarth;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _attackerEarth = value;
                NotifyPropertyChanged();
            }
        }

        public int AttackerMetal
        {
            get
            {
                return _attackerMetal;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _attackerMetal = value;
                NotifyPropertyChanged();
            }
        }
        
        public MaterialData SelectedWeaponMaterial
        {
            get
            {
                return _selectedWeaponMaterial;
            }
            set
            {
                _selectedWeaponMaterial = value;
                NotifyPropertyChanged();
                SetWeaponPower();
            }
        }

        public int EnemyCurrentHp
        {
            get
            {
                return _enemyCurrentHp;
            }
            set
            {
                if (value < 0)
                    return;
                _enemyCurrentHp = value;
                NotifyPropertyChanged();
            }
        }

        public int EnemyMaxHp
        {
            get
            {
                return _enemyMaxHp;
            }
            set
            {
                if (value < 1)
                    return;
                _enemyMaxHp = value;
                NotifyPropertyChanged();
            }
        }

        public int EnemyLp
        {
            get
            {
                return _enemyLp;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _enemyLp = value;
                NotifyPropertyChanged();
            }
        }

        public int EnemySlashDefense
        {
            get
            {
                return _enemySlashDefense;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _enemySlashDefense = value;
                NotifyPropertyChanged();
            }
        }

        public int EnemyHitDefense
        {
            get
            {
                return _enemyHitDefense;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _enemyHitDefense = value;
                NotifyPropertyChanged();
            }
        }

        public int EnemyPierceDefense
        {
            get 
            {
                return _enemyPierceDefense;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _enemyPierceDefense = value;
                NotifyPropertyChanged();
            }
        }

        public int EnemyHeatDefense
        {
            get
            {
                return _enemyHeatDefense;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _enemyHeatDefense = value;
                NotifyPropertyChanged();
            }
        }

        public int EnemyColdDefense
        {
            get
            {
                return _enemyColdDefense;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _enemyColdDefense = value;
                NotifyPropertyChanged();
            }
        }

        public int EnemyLightningDefense
        {
            get
            {
                return _enemyLightningDefense;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _enemyLightningDefense = value;
                NotifyPropertyChanged();
            }
        }

        public int EnemyLightDefense 
        {
            get
            {
                return _enemyLightDefense;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _enemyLightDefense = value;
                NotifyPropertyChanged();
            }
        }

        public int EnemyLpDefense
        {
            get
            {
                return _enemyLpDefense;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _enemyLpDefense = value;
                NotifyPropertyChanged();
            }
        }

        public EnemyData SelectedEnemy
        {
            get
            {
                return _selectedEnemy;
            }
            set
            {
                _selectedEnemy = value;
                NotifyPropertyChanged();
                UpdateEnemyData();
            }
        }

        public AttackType AttackType
        {
            get
            {
                return _attackType;
            }
            set
            {
                _attackType = value;
                NotifyPropertyChanged();
            }
        }

        public WeaponType WeaponType
        {
            get
            {
                return _weaponType;
            }
            set
            {
                _weaponType = value;
                NotifyPropertyChanged();
            }
        }

        public MagicType MagicType
        {
            get
            {
                return _magicType;
            }
            set
            {
                _magicType = value;
                NotifyPropertyChanged();
            }
        }

        public int EnCost
        {
            get
            {
                return _enCost;
            }
            set
            {
                if (value < 0 || value > 2)
                    return;
                _enCost = value;
                NotifyPropertyChanged();
            }
        }

        public int GrowthRate
        {
            get
            {
                return _growthRate;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _growthRate = value;
                NotifyPropertyChanged();
            }
        }

        public int HpDamage
        {
            get
            {
                return _hpDamage;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _hpDamage = value;
                NotifyPropertyChanged();
            }
        }

        public int LpDamage
        {
            get
            {
                return _lpDamage;
            }
            set
            {
                if (value < 0 || value > 255)
                    return;
                _lpDamage = value;
                NotifyPropertyChanged();
            }
        }

        public int NumberOfHits
        {
            get
            {
                return _numberOfHits;
            }
            set
            {
                if (value < 1 || value > 255)
                    return;
                _numberOfHits = value;
                NotifyPropertyChanged();
            }
        }

        public string AttackAttributesString
        {
            get
            {
                return _attackAttributesString;
            }
            set
            {
                _attackAttributesString = value;
                NotifyPropertyChanged();
            }
        }

        public AttackData SelectedAttack
        {
            get
            {
                return _selectedAttack;
            }
            set
            {
                _selectedAttack = value;
                NotifyPropertyChanged();
                UpdateAttackData();
            }
        }

        public string RawDamageString
        {
            get
            {
                return _rawDamageString;
            }
            set
            {
                _rawDamageString = value;
                NotifyPropertyChanged();
            }
        }

        public string FinalDamageString
        {
            get
            {
                return _finalDamageString;
            }
            set
            {
                _finalDamageString = value;
                NotifyPropertyChanged();
            }
        }

        public double ComboPercentage
        { 
            get
            {
                return _comboPercentage;
            }
            set
            {
                if (value < 1.0 || value > 2.0)
                    return;
                _comboPercentage = value;
                NotifyPropertyChanged();
            }
        }

        public string DamageProbability
        {
            get
            {
                return _damageProbability;
            }
            set
            {
                _damageProbability = value;
                NotifyPropertyChanged();
            }
        }

        public string OddsOfNoDamage
        {
            get
            {
                return _oddsOfNoDamage;
            }
            set
            {
                _oddsOfNoDamage = value;
                NotifyPropertyChanged();
            }
        }

        public string OddsOfOneDamage
        {
            get
            {
                return _oddsOfOneDamage;
            }
            set
            {
                _oddsOfOneDamage = value;
                NotifyPropertyChanged();
            }
        }

        public string OddsOfTwoDamage
        {
            get
            {
                return _oddsOfTwoDamage;
            }
            set
            {
                _oddsOfTwoDamage = value;
                NotifyPropertyChanged();
            }
        }

        public string OddsOfThreeDamage
        {
            get
            { 
                return _oddsOfThreeDamage;
            }
            set
            {
                _oddsOfThreeDamage = value;
                NotifyPropertyChanged();
            }
        }

        public int AttackDamage
        {
            get
            {
                return _attackDamage;
            }
            set
            {
                _attackDamage = value;
                NotifyPropertyChanged();
            }
        }

        public string AttackTypeString
        {
            get
            {
                return _attackTypeString;
            }
            set
            {
                _attackTypeString = value;
                NotifyPropertyChanged();
            }
        }

        public string WeaponTypeString
        {
            get
            {
                return _weaponTypeString;
            }
            set
            {
                _weaponTypeString = value;
                NotifyPropertyChanged();
            }
        }

        public string MagicTypeString
        {
            get
            {
                return _magicTypeString;
            }
            set
            {
                _magicTypeString = value;
                NotifyPropertyChanged();
            }
        }

        public WeaponType SelectedWeaponTypeFilter
        {
            get
            {
                return _selectedWeaponTypeFilter;
            }
            set
            {
                _selectedWeaponTypeFilter = value;
                NotifyPropertyChanged();
                RepopulateAttacks();
                RepopulateWeaponMaterials();
            }
        }

        public EnemyCategory SelectedEnemyCategoryFilter
        {
            get
            {
                return _selectedEnemyCategoryFilter;
            }
            set
            {
                _selectedEnemyCategoryFilter = value;
                NotifyPropertyChanged();
                RepopulateEnemies();
            }
        }

        public int ComboDamage
        {
            get
            {
                return _comboDamage;
            }
            set
            {
                if (value < 0 || value > 9999)
                    return;
                _comboDamage = value;
                NotifyPropertyChanged();
            }
        }

        public DamageCalculatorController(IDamageCalculatorView view, IUsagaModel usagaModel, IHpDamageCalculator hpDamageCalculator, ILpDamageCalculator lpDamageCalculator)
        {
            View = view;
            UsagaModel = usagaModel;
            HpDamageCalculator = hpDamageCalculator;
            LpDamageCalculator = lpDamageCalculator;

            RepopulateEnemyCategories();
            RepopulateWeaponTypes();
            RepopulateAttacks();
            RepopulateEnemies();
            View.SetController(this);
        }

        private void RepopulateWeaponMaterials()
        {
            WeaponMaterials.Clear();

            switch (SelectedWeaponTypeFilter)
            {
                case WeaponType.Punch:
                    break;
                case WeaponType.Kick:
                    break;
                case WeaponType.Throw:
                    break;
                case WeaponType.Dagger:
                    foreach (var material in UsagaModel.MaterialCollection.Materials.Where(m => m.DaggerDamage > 0))
                    {
                        WeaponMaterials.Add(material);
                    }
                    break;
                case WeaponType.Sword:
                    foreach (var material in UsagaModel.MaterialCollection.Materials.Where(m => m.SwordDamage > 0))
                    {
                        WeaponMaterials.Add(material);
                    }
                    break;
                case WeaponType.Axe:
                    foreach (var material in UsagaModel.MaterialCollection.Materials.Where(m => m.AxeDamage > 0))
                    {
                        WeaponMaterials.Add(material);
                    }
                    break;
                case WeaponType.Staff:
                    foreach (var material in UsagaModel.MaterialCollection.Materials.Where(m => m.StaffDamage > 0))
                    {
                        WeaponMaterials.Add(material);
                    }
                    break;
                case WeaponType.Spear:
                    foreach (var material in UsagaModel.MaterialCollection.Materials.Where(m => m.SpearDamage > 0))
                    {
                        WeaponMaterials.Add(material);
                    }
                    break;
                case WeaponType.Bow:
                    foreach (var material in UsagaModel.MaterialCollection.Materials.Where(m => m.BowDamage > 0))
                    {
                        WeaponMaterials.Add(material);
                    }
                    break;
                case WeaponType.Gun:
                    foreach (var material in UsagaModel.MaterialCollection.Materials.Where(m => m.GunDamage > 0))
                    {
                        WeaponMaterials.Add(material);
                    }
                    break;
                case WeaponType.Magic:
                    break;
            }
        }

        private void SetWeaponPower()
        {
            if (SelectedWeaponMaterial == null)
                return;

            switch (SelectedWeaponTypeFilter)
            {
                case WeaponType.Punch:
                    break;
                case WeaponType.Kick:
                    break;
                case WeaponType.Throw:
                    break;
                case WeaponType.Dagger:
                    AttackerWeaponPower = SelectedWeaponMaterial.DaggerDamage;
                    break;
                case WeaponType.Sword:
                    AttackerWeaponPower = SelectedWeaponMaterial.SwordDamage;
                    break;
                case WeaponType.Axe:
                    AttackerWeaponPower = SelectedWeaponMaterial.AxeDamage;
                    break;
                case WeaponType.Staff:
                    AttackerWeaponPower = SelectedWeaponMaterial.StaffDamage;
                    break;
                case WeaponType.Spear:
                    AttackerWeaponPower = SelectedWeaponMaterial.SpearDamage;
                    break;
                case WeaponType.Bow:
                    AttackerWeaponPower = SelectedWeaponMaterial.BowDamage;
                    break;
                case WeaponType.Gun:
                    AttackerWeaponPower = SelectedWeaponMaterial.GunDamage;
                    break;
                case WeaponType.Magic:
                    break;
            }
        }

        private void RepopulateEnemies()
        {
            Enemies.Clear();
            foreach (var enemy in UsagaModel.EnemyCollection.Enemies.Where(e => e.Category == SelectedEnemyCategoryFilter))
            {
                Enemies.Add(enemy);
            }
            if (Enemies.Count > 0)
            {
                SelectedEnemy = Enemies[0];
            }
        }

        private void RepopulateAttacks()
        {
            Attacks.Clear();
            foreach (var weapon in UsagaModel.AttackCollection.Attacks.Where(a => a.WeaponType == SelectedWeaponTypeFilter))
            {
                Attacks.Add(weapon);
            }
            if (Attacks.Count > 0)
            {
                SelectedAttack = Attacks[0];
            }
        }

        private void RepopulateEnemyCategories()
        {
            EnemyCategories.Clear();
            foreach (var enemyCategory in Enum.GetValues(typeof(EnemyCategory)))
            {
                EnemyCategories.Add((EnemyCategory)enemyCategory, Properties.Resources.ResourceManager.GetString(enemyCategory.ToString()));
            }
        }

        private void RepopulateWeaponTypes()
        {
            WeaponTypes.Clear();
            foreach (var weaponType in Enum.GetValues(typeof(WeaponType)))
            {
                WeaponTypes.Add((WeaponType)weaponType, Properties.Resources.ResourceManager.GetString(weaponType.ToString()));
            }
        }

        private void UpdateAttackData()
        {
            if (_selectedAttack == null)
                return;

            AttackType = _selectedAttack.AttackType;
            WeaponType = _selectedAttack.WeaponType;
            MagicType = _selectedAttack.MagicType;
            EnCost = (int)_selectedAttack.EnCost;
            GrowthRate = (int)_selectedAttack.GrowthRate;
            HpDamage = (int)_selectedAttack.HpDamage;
            LpDamage = (int)_selectedAttack.LpDamage;
            NumberOfHits = (int)_selectedAttack.NumberOfHits;
            _attackAttributes = _selectedAttack.Attributes;
            if (AttackType == AttackType.Martial)
                View.BoldMartialFields();
            else if (AttackType == AttackType.Strength)
                View.BoldStrengthFields();
            else if (AttackType == AttackType.Skill)
                View.BoldSkillFields();
            else
                View.BoldMagicFields(MagicType);

            UpdateLocalizedData();
        }

        private void UpdateLocalizedData()
        {
            if (_selectedAttack != null)
            {
                AttackTypeString = Properties.Resources.ResourceManager.GetString(_selectedAttack.AttackType.ToString());
                WeaponTypeString = Properties.Resources.ResourceManager.GetString(_selectedAttack.WeaponType.ToString());
                MagicTypeString = Properties.Resources.ResourceManager.GetString(_selectedAttack.MagicType.ToString());

                AttackAttributesString = "";
                foreach (var attribute in _selectedAttack.Attributes)
                {
                    AttackAttributesString += Properties.Resources.ResourceManager.GetString(attribute.ToString()) + " ";
                }
            }
            RepopulateEnemyCategories();
            RepopulateWeaponTypes();
        }

        private void UpdateEnemyData()
        {
            if (_selectedEnemy == null)
            {
                return;
            }
            EnemyCurrentHp = (int)_selectedEnemy.Hp;
            EnemyMaxHp = (int)_selectedEnemy.Hp;
            EnemyLp = (int)_selectedEnemy.Lp;
            EnemySlashDefense = (int)_selectedEnemy.SlashDefense;
            EnemyHitDefense = (int)_selectedEnemy.HitDefense;
            EnemyPierceDefense = (int)_selectedEnemy.PierceDefense;
            EnemyHeatDefense = (int)_selectedEnemy.HeatDefense;
            EnemyColdDefense = (int)_selectedEnemy.ColdDefense;
            EnemyLightningDefense = (int)_selectedEnemy.LightningDefense;
            EnemyLightDefense = (int)_selectedEnemy.LightDefense;
            EnemyLpDefense = (int)_selectedEnemy.LpDefense;
        }

        public void CalculateHpDamage()
        {
            HpDamageResult result;
            if (_attackType == AttackType.Martial)
                result = HpDamageCalculator.CalculateMartialDamage(GetAttackerData(), GetEnemyData(), GetAttackData(), ComboPercentage);
            else if (_attackType == AttackType.Skill)
                result = HpDamageCalculator.CalculateSkillDamage(GetAttackerData(), GetEnemyData(), GetAttackData(), ComboPercentage);
            else if (_attackType == AttackType.Strength)
                result = HpDamageCalculator.CalculateStrengthDamage(GetAttackerData(), GetEnemyData(), GetAttackData(), ComboPercentage);
            else
                result = HpDamageCalculator.CalculateMagicDamage(GetAttackerData(), GetEnemyData(), GetAttackData(), ComboPercentage);

            RawDamageString = $"{result.RawDamageLower} - {result.RawDamageUpper}";
            FinalDamageString = $"{result.FinalDamageLower} - {result.FinalDamageUpper}";
            _finalDamage = result.FinalDamageLower;
        }

        public void CalculateLpDamage()
        {
            var enemyData = GetEnemyData();
            if (ComboDamage > 0)
            {
                enemyData.CurrentHp -= ComboDamage;
            }
            else
            {
                enemyData.CurrentHp -= AttackDamage;
            }
            var result = LpDamageCalculator.CalculateLpDamageResult(GetAttackData(), enemyData, AttackDamage, ComboPercentage);

            DamageProbability = result.DamageProbability.ToString("0.000");
            OddsOfNoDamage = result.OddsOfNoDamage.ToString("0.000");
            OddsOfOneDamage = result.OddsOfOneDamage.ToString("0.000");
            OddsOfTwoDamage = result.OddsOfTwoDamage.ToString("0.000");
            OddsOfThreeDamage = result.OddsOfThreeDamage.ToString("0.000");
        }

        private EnemyData GetEnemyData()
        {
            return new EnemyData
            {
                CurrentHp = EnemyCurrentHp,
                Hp = EnemyMaxHp,
                Lp = EnemyLp,
                SlashDefense = EnemySlashDefense,
                HitDefense = EnemyHitDefense,
                PierceDefense = EnemyPierceDefense,
                HeatDefense = EnemyHeatDefense,
                ColdDefense = EnemyColdDefense,
                LightningDefense = EnemyLightningDefense,
                LightDefense = EnemyLightDefense,
                LpDefense = EnemyLpDefense
            };
        }

        private AttackData GetAttackData()
        {
            return new AttackData
            {
                AttackType = _attackType,
                WeaponType = _weaponType,
                MagicType = _magicType,
                EnCost = EnCost,
                GrowthRate = GrowthRate,
                HpDamage = HpDamage,
                LpDamage = LpDamage,
                NumberOfHits = NumberOfHits,
                Attributes = _attackAttributes
            };
        }

        private AttackerData GetAttackerData()
        {
            return new AttackerData
            {
                CurrentHp = AttackerCurrentHp,
                MaxHp = AttackerMaxHp,
                Strength = AttackerStrength,
                Skill = AttackerSkill,
                Weight = AttackerWeight,
                WeaponAttackPower = AttackerWeaponPower,
                Magic = AttackerMagic,
                Fire = AttackerFire,
                Water = AttackerWater,
                Wood = AttackerWood,
                Earth = AttackerEarth,
                Metal = AttackerMetal
            };
        }

        public void UpdateLanguage()
        {
            UpdateLocalizedData();
            View.UpdateLanguage();
        }

        public void TransferCharacterData(CharacterData characterData)
        {
            AttackerStrength = (int)characterData.Strength;
            AttackerSkill = (int)characterData.Skill;
            AttackerWeight = (int)characterData.Weight;
            AttackerMagic = (int)characterData.Magic;
            AttackerFire = (int)characterData.Fire;
            AttackerWater = (int)characterData.Water;
            AttackerWood = (int)characterData.Wood;
            AttackerEarth = (int)characterData.Earth;
            AttackerMetal = (int)characterData.Metal;
            AttackerCurrentHp = characterData.Hp;
            AttackerMaxHp = characterData.Hp;
        }

        public void SetDamage()
        {
            AttackDamage = _finalDamage;
        }

        public void AddToCombo()
        {
            ComboDamage += _finalDamage;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
