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
    internal class CharacterStatCalculatorController : ICharacterStatCalculatorController, INotifyPropertyChanged
    {

        private ICharacterStatCalculatorView View { get; }
        private IUsagaModel UsagaModel { get; }
        private ICharacterStatCalculator CharacterStatCalculator { get; }
        private IDamageCalculatorController DamageCalculatorController { get; }
        private IMainController MainController { get; set; }

        public List<CharacterData> Characters => UsagaModel.CharacterCollection.Characters;
        private List<PanelData> Panels => UsagaModel.PanelCollection.Panels;

        public Dictionary<PanelType,string> PanelTypes { get; } = new Dictionary<PanelType, string>();
        public List<int> PanelLevels { get; } = new List<int> { 0, 1, 2, 3, 4, 5 };

        public ObservableCollection<PanelData> NWPanels { get; } = new ObservableCollection<PanelData>();
        public ObservableCollection<PanelData> NEPanels { get; } = new ObservableCollection<PanelData>();
        public ObservableCollection<PanelData> WPanels { get; } = new ObservableCollection<PanelData>();
        public ObservableCollection<PanelData> CPanels { get; } = new ObservableCollection<PanelData>();
        public ObservableCollection<PanelData> EPanels { get; } = new ObservableCollection<PanelData>();
        public ObservableCollection<PanelData> SWPanels { get; } = new ObservableCollection<PanelData>();
        public ObservableCollection<PanelData> SEPanels { get; } = new ObservableCollection<PanelData>();

        private CharacterData _selectedCharacter;
        private int _characterLp;
        private int _characterStrength;
        private int _characterSkill;
        private int _characterMagic;
        private int _characterEndurance;
        private int _characterSpirit;
        private int _characterFire;
        private int _characterEarth;
        private int _characterMetal;
        private int _characterWater;
        private int _characterWood;
        private int _characterWeight;
        private int _characterHp;
        private bool _prioritizeLine;
        private string _linePrioritiesString;
        private string _jointPrioritiesString;
        private string _elementOneString;
        private string _elementTwoString;
        private string _elementThreeString;
        private string _elementFourString;
        private string _elementFiveString;

        private PanelType _nwSelectedType;
        private PanelType _neSelectedType;
        private PanelType _wSelectedType;
        private PanelType _cSelectedType;
        private PanelType _eSelectedType;
        private PanelType _swSelectedType;
        private PanelType _seSelectedType;
        private PanelData _nwSelectedPanel;
        private PanelData _neSelectedPanel;
        private PanelData _wSelectedPanel;
        private PanelData _cSelectedPanel;
        private PanelData _eSelectedPanel;
        private PanelData _swSelectedPanel;
        private PanelData _seSelectedPanel;
        private int _nwSelectedLevel;
        private int _neSelectedLevel;
        private int _wSelectedLevel;
        private int _cSelectedLevel;
        private int _eSelectedLevel;
        private int _swSelectedLevel;
        private int _seSelectedLevel;

        private int _rawStrength;
        private int _bonusStrength;
        private int _finalStrength;
        private int _rawSkill;
        private int _bonusSkill;
        private int _finalSkill;
        private int _rawMagic;
        private int _bonusMagic;
        private int _finalMagic;
        private int _rawEndurance;
        private int _bonusEndurance;
        private int _finalEndurance;
        private int _rawSpirit;
        private int _bonusSpirit;
        private int _finalSpirit;
        private int _rawFire;
        private int _bonusFire;
        private int _finalFire;
        private int _rawEarth;
        private int _bonusEarth;
        private int _finalEarth;
        private int _rawMetal;
        private int _bonusMetal;
        private int _finalMetal;
        private int _rawWater;
        private int _bonusWater;
        private int _finalWater;
        private int _rawWood;
        private int _bonusWood;
        private int _finalWood;
        
        public CharacterData SelectedCharacter
        {
            get {
                return _selectedCharacter;
            }
            set
            {
                _selectedCharacter = value;
                NotifyPropertyChanged();
                UpdateCharacterData();
            }
        }

        public int CharacterLp
        {
            get
            {
                return _characterLp;
            }
            set
            {
                if (value > 100 || value < 1)
                    return;
                _characterLp = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterStrength
        {
            get
            {
                return _characterStrength;
            }
            set
            {
                if (value > 5 || value < 1)
                    return;
                _characterStrength = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterSkill
        {
            get
            {
                return _characterSkill;
            }
            set
            {
                if (value > 5 || value < 1)
                    return;
                _characterSkill = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterMagic
        {
            get
            {
                return _characterMagic;
            }
            set
            {
                if (value > 5 || value < 1)
                    return;
                _characterMagic = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterEndurance
        {
            get
            {
                return _characterEndurance;
            }
            set
            {
                if (value > 5 || value < 1)
                    return;
                _characterEndurance = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterSpirit
        {
            get
            {
                return _characterSpirit;
            }
            set
            {
                if (value > 5 || value < 1)
                    return;
                _characterSpirit = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterFire
        {
            get
            {
                return _characterFire;
            }
            set
            {
                if (value > 4 || value < 0)
                    return;
                _characterFire = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterEarth
        {
            get
            {
                return _characterEarth;
            }
            set
            {
                if (value > 4 || value < 0)
                    return;
                _characterEarth = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterMetal
        {
            get
            {
                return _characterMetal;
            }
            set
            {
                if (value > 4 || value < 0)
                    return;
                _characterMetal = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterWater
        {
            get
            {
                return _characterWater;
            }
            set
            {
                if (value > 4 || value < 0)
                    return;
                _characterWater = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterWood
        {
            get
            {
                return _characterWood;
            }
            set
            {
                if (value > 4 || value < 0)
                    return;
                _characterWood = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterWeight
        {
            get
            {
                return _characterWeight;
            }
            set
            {
                if (value > 255 || value < 0)
                    return;
                _characterWeight = value;
                NotifyPropertyChanged();
            }
        }

        public int CharacterHp
        {
            get
            {
                return _characterHp;
            }
            set
            {
                _characterHp = value;
                NotifyPropertyChanged();
            }
        }
        public bool PrioritizeLine
        {
            get
            {
                return _prioritizeLine;
            }
            set
            {
                _prioritizeLine = value;
                NotifyPropertyChanged();
            }
        }

        public string LinePrioritiesString
        {
            get
            {
                return _linePrioritiesString;
            }
            set
            {
                _linePrioritiesString = value;
                NotifyPropertyChanged();
            }
        }

        public string JointPrioritiesString
        {
            get
            {
                return _jointPrioritiesString;
            }
            set
            {
                _jointPrioritiesString = value;
                NotifyPropertyChanged();
            }
        }

        public PanelType NwSelectedType
        {
            get
            {
                return _nwSelectedType;
            }
            set
            {
                _nwSelectedType = value;
                NotifyPropertyChanged();
                SetPanelSelection(value, NWPanels);
                if (NWPanels.Count > 0)
                {
                    NwSelectedPanel = NWPanels[0];
                    NwSelectedLevel = 1;
                }
                else
                {
                    NwSelectedLevel = 0;
                }
            }
        }

        public PanelType NeSelectedType
        {
            get
            {
                return _neSelectedType;
            }
            set
            {
                _neSelectedType = value;
                NotifyPropertyChanged();
                SetPanelSelection(value, NEPanels);
                if (NEPanels.Count > 0)
                {
                    NeSelectedPanel = NEPanels[0];
                    NeSelectedLevel = 1;
                }
                else
                {
                    NeSelectedLevel = 0;
                }
            }
        }

        public PanelType WSelectedType
        {
            get
            {
                return _wSelectedType;
            }
            set
            {
                _wSelectedType = value;
                NotifyPropertyChanged();
                SetPanelSelection(value, WPanels);
                if (WPanels.Count > 0)
                {
                    WSelectedPanel = WPanels[0];
                    WSelectedLevel = 1;
                }
                else
                {
                    WSelectedLevel = 0;
                }
            }
        }

        public PanelType CSelectedType
        {
            get
            {
                return _cSelectedType;
            }
            set
            {
                _cSelectedType = value;
                NotifyPropertyChanged();
                SetPanelSelection(value, CPanels);
                if (CPanels.Count > 0)
                {
                    CSelectedPanel = CPanels[0];
                    CSelectedLevel = 1;
                }
                else
                {
                    CSelectedLevel = 0;
                }
            }
        }

        public PanelType ESelectedType
        {
            get
            {
                return _eSelectedType;
            }
            set
            {
                _eSelectedType = value;
                NotifyPropertyChanged();
                SetPanelSelection(value, EPanels);
                if (EPanels.Count > 0)
                {
                    ESelectedPanel = EPanels[0];
                    ESelectedLevel = 1;
                }
                else
                {
                    ESelectedLevel = 0;
                }
            }
        }

        public PanelType SwSelectedType
        {
            get
            {
                return _swSelectedType;
            }
            set
            {
                _swSelectedType = value;
                NotifyPropertyChanged();
                SetPanelSelection(value, SWPanels);
                if (SWPanels.Count > 0)
                {
                    SwSelectedPanel = SWPanels[0];
                    SwSelectedLevel = 1;
                }
                else
                {
                    SwSelectedLevel = 0;
                }
            }
        }

        public PanelType SeSelectedType
        {
            get
            {
                return _seSelectedType;
            }
            set
            {
                _seSelectedType = value;
                NotifyPropertyChanged();
                SetPanelSelection(value, SEPanels);
                if (SEPanels.Count > 0)
                {
                    SeSelectedPanel = SEPanels[0];
                    SeSelectedLevel = 1;
                }
                else
                {
                    SeSelectedLevel = 0;
                }
            }
        }

        public PanelData NwSelectedPanel
        {
            get
            {
                return _nwSelectedPanel;
            }
            set
            {
                _nwSelectedPanel = value;
                NotifyPropertyChanged();
            }
        }

        public PanelData NeSelectedPanel
        {
            get
            {
                return _neSelectedPanel;
            }
            set
            {
                _neSelectedPanel = value;
                NotifyPropertyChanged();
            }
        }

        public PanelData WSelectedPanel
        {
            get
            {
                return _wSelectedPanel;
            }
            set
            {
                _wSelectedPanel = value;
                NotifyPropertyChanged();
            }
        }

        public PanelData CSelectedPanel
        {
            get
            {
                return _cSelectedPanel;
            }
            set
            {
                _cSelectedPanel = value;
                NotifyPropertyChanged();
            }
        }

        public PanelData ESelectedPanel
        {
            get
            {
                return _eSelectedPanel;
            }
            set
            {
                _eSelectedPanel = value;
                NotifyPropertyChanged();
            }
        }

        public PanelData SwSelectedPanel
        {
            get
            {
                return _swSelectedPanel;
            }
            set
            {
                _swSelectedPanel = value;
                NotifyPropertyChanged();
            }
        }

        public PanelData SeSelectedPanel
        {
            get
            {
                return _seSelectedPanel;
            }
            set
            {
                _seSelectedPanel = value;
                NotifyPropertyChanged();
            }
        }

        public int NwSelectedLevel
        {
            get
            {
                return _nwSelectedLevel;
            }
            set
            {
                _nwSelectedLevel = value;
                NotifyPropertyChanged();
            }
        }

        public int NeSelectedLevel
        {
            get
            {
                return _neSelectedLevel;
            }
            set
            {
                _neSelectedLevel = value;
                NotifyPropertyChanged();
            }
        }

        public int WSelectedLevel
        {
            get
            {
                return _wSelectedLevel;
            }
            set
            {
                _wSelectedLevel = value;
                NotifyPropertyChanged();
            }
        }

        public int CSelectedLevel
        {
            get
            {
                return _cSelectedLevel;
            }
            set
            {
                _cSelectedLevel = value;
                NotifyPropertyChanged();
            }
        }

        public int ESelectedLevel
        {
            get
            {
                return _eSelectedLevel;
            }
            set
            {
                _eSelectedLevel = value;
                NotifyPropertyChanged();
            }
        }

        public int SwSelectedLevel
        {
            get
            {
                return _swSelectedLevel;
            }
            set
            {
                _swSelectedLevel = value;
                NotifyPropertyChanged();
            }
        }

        public int SeSelectedLevel
        {
            get
            {
                return _seSelectedLevel;
            }
            set
            {
                _seSelectedLevel = value;
                NotifyPropertyChanged();
            }
        }

        public int RawStrength
        {
            get
            {
                return _rawStrength;
            }
            set
            {
                _rawStrength = value;
                NotifyPropertyChanged();
            }
        }

        public int BonusStrength
        {
            get
            {
                return _bonusStrength;
            }
            set
            {
                _bonusStrength = value;
                NotifyPropertyChanged();
            }
        }

        public int FinalStrength
        {
            get
            {
                return _finalStrength;
            }
            set
            {
                _finalStrength = value;
                NotifyPropertyChanged();
            }
        }

        public int RawSkill
        {
            get
            {
                return _rawSkill;
            }
            set
            {
                _rawSkill = value;
                NotifyPropertyChanged();
            }
        }

        public int BonusSkill
        {
            get
            {
                return _bonusSkill;
            }
            set
            {
                _bonusSkill = value;
                NotifyPropertyChanged();
            }
        }

        public int FinalSkill
        {
            get
            {
                return _finalSkill;
            }
            set
            {
                _finalSkill = value;
                NotifyPropertyChanged();
            }
        }

        public int RawMagic
        {
            get
            {
                return _rawMagic;
            }
            set
            {
                _rawMagic = value;
                NotifyPropertyChanged();
            }
        }

        public int BonusMagic
        {
            get
            {
                return _bonusMagic;
            }
            set
            {
                _bonusMagic = value;
                NotifyPropertyChanged();
            }
        }

        public int FinalMagic
        {
            get
            {
                return _finalMagic;
            }
            set
            {
                _finalMagic = value;
                NotifyPropertyChanged();
            }
        }

        public int RawEndurance
        {
            get
            {
                return _rawEndurance;
            }
            set
            {
                _rawEndurance = value;
                NotifyPropertyChanged();
            }
        }

        public int BonusEndurance
        {
            get
            {
                return _bonusEndurance;
            }
            set
            {
                _bonusEndurance = value;
                NotifyPropertyChanged();
            }
        }

        public int FinalEndurance
        {
            get
            {
                return _finalEndurance;
            }
            set
            {
                _finalEndurance = value;
                NotifyPropertyChanged();
            }
        }

        public int RawSpirit
        {
            get
            {
                return _rawSpirit;
            }
            set
            {
                _rawSpirit = value;
                NotifyPropertyChanged();
            }
        }

        public int BonusSpirit
        {
            get
            {
                return _bonusSpirit;
            }
            set
            {
                _bonusSpirit = value;
                NotifyPropertyChanged();
            }
        }

        public int FinalSpirit
        {
            get
            {
                return _finalSpirit;
            }
            set
            {
                _finalSpirit = value;
                NotifyPropertyChanged();
            }
        }

        public int RawFire
        {
            get
            {
                return _rawFire;
            }
            set
            {
                _rawFire = value;
                NotifyPropertyChanged();
            }
        }

        public int BonusFire
        {
            get
            {
                return _bonusFire;
            }
            set
            {
                _bonusFire = value;
                NotifyPropertyChanged();
            }
        }

        public int FinalFire
        {
            get
            {
                return _finalFire;
            }
            set
            {
                _finalFire = value;
                NotifyPropertyChanged();
            }
        }

        public int RawEarth
        {
            get
            {
                return _rawEarth;
            }
            set
            {
                _rawEarth = value;
                NotifyPropertyChanged();
            }
        }

        public int BonusEarth
        {
            get
            {
                return _bonusEarth;
            }
            set
            {
                _bonusEarth = value;
                NotifyPropertyChanged();
            }
        }

        public int FinalEarth
        {
            get
            {
                return _finalEarth;
            }
            set
            {
                _finalEarth = value;
                NotifyPropertyChanged();
            }
        }

        public int RawMetal
        {
            get
            {
                return _rawMetal;
            }
            set
            {
                _rawMetal = value;
                NotifyPropertyChanged();
            }
        }

        public int BonusMetal
        {
            get
            {
                return _bonusMetal;
            }
            set
            {
                _bonusMetal = value;
                NotifyPropertyChanged();
            }
        }

        public int FinalMetal
        {
            get
            {
                return _finalMetal;
            }
            set
            {
                _finalMetal = value;
                NotifyPropertyChanged();
            }
        }

        public int RawWater
        {
            get
            {
                return _rawWater;
            }
            set
            {
                _rawWater = value;
                NotifyPropertyChanged();
            }
        }

        public int BonusWater
        {
            get
            {
                return _bonusWater;
            }
            set
            {
                _bonusWater = value;
                NotifyPropertyChanged();
            }
        }

        public int FinalWater
        {
            get
            {
                return _finalWater;
            }
            set
            {
                _finalWater = value;
                NotifyPropertyChanged();
            }
        }

        public int RawWood
        {
            get
            {
                return _rawWood;
            }
            set
            {
                _rawWood = value;
                NotifyPropertyChanged();
            }
        }

        public int BonusWood
        {
            get
            {
                return _bonusWood;
            }
            set
            {
                _bonusWood = value;
                NotifyPropertyChanged();
            }
        }

        public int FinalWood
        {
            get
            {
                return _finalWood;
            }
            set
            {
                _finalWood = value;
                NotifyPropertyChanged();
            }
        }

        public string ElementOneString
        {
            get
            {
                return _elementOneString;
            }
            set
            {
                _elementOneString = value;
                NotifyPropertyChanged();
            }
        }

        public string ElementTwoString
        {
            get
            {
                return _elementTwoString;
            }
            set
            {
                _elementTwoString = value;
                NotifyPropertyChanged();
            }
        }

        public string ElementThreeString
        {
            get
            {
                return _elementThreeString;
            }
            set
            {
                _elementThreeString = value;
                NotifyPropertyChanged();
            }
        }

        public string ElementFourString
        {
            get
            {
                return _elementFourString;
            }
            set
            {
                _elementFourString = value;
                NotifyPropertyChanged();
            }
        }

        public string ElementFiveString
        {
            get
            {
                return _elementFiveString;
            }
            set
            {
                _elementFiveString = value;
                NotifyPropertyChanged();
            }
        }

        public CharacterStatCalculatorController(ICharacterStatCalculatorView view, IUsagaModel usagaModel, ICharacterStatCalculator characterStatCalculator, IDamageCalculatorController damageCalculatorController)
        {
            View = view;
            UsagaModel = usagaModel;
            CharacterStatCalculator = characterStatCalculator;
            DamageCalculatorController = damageCalculatorController;

            ReloadPanelTypes();
            View.SetController(this);
        }

        private void UpdateCharacterData()
        {
            if (SelectedCharacter == null)
                return;
            CharacterLp = SelectedCharacter.Lp;
            CharacterStrength = (int)SelectedCharacter.Strength;
            CharacterSkill = (int)SelectedCharacter.Skill;
            CharacterMagic = (int)SelectedCharacter.Magic;
            CharacterEndurance = (int)SelectedCharacter.Endurance;
            CharacterSpirit = (int)SelectedCharacter.Spirit;
            CharacterFire = (int)SelectedCharacter.Fire;
            CharacterEarth = (int)SelectedCharacter.Earth;
            CharacterMetal = (int)SelectedCharacter.Metal;
            CharacterWater = (int)SelectedCharacter.Water;
            CharacterWood = (int)SelectedCharacter.Wood;
            CharacterWeight = (int)SelectedCharacter.Weight;
            CharacterHp = SelectedCharacter.Hp;

            ElementOneString = Properties.Resources.ResourceManager.GetString(SelectedCharacter.FirstElement);
            ElementTwoString = Properties.Resources.ResourceManager.GetString(SelectedCharacter.SecondElement);
            ElementThreeString = Properties.Resources.ResourceManager.GetString(SelectedCharacter.ThirdElement);
            ElementFourString = Properties.Resources.ResourceManager.GetString(SelectedCharacter.FourthElement);
            ElementFiveString = Properties.Resources.ResourceManager.GetString(SelectedCharacter.FifthElement);
        }

        private void SetPanelSelection(PanelType selectedType, ObservableCollection<PanelData> collectionToUpdate)
        {
            collectionToUpdate.Clear();
            foreach (var panel in UsagaModel.PanelCollection.Panels.Where(p => p.PanelType.Equals(selectedType)))
            {
                collectionToUpdate.Add(panel);
            }
        }

        public void SetStartingPanels()
        {
            if (SelectedCharacter == null)
                return;

            var nwPanel = Panels.FirstOrDefault(p => p.EnglishName.Equals(SelectedCharacter.NWPanel));
            if (nwPanel != null)
            {
                NwSelectedType = nwPanel.PanelType;
                NwSelectedPanel = nwPanel;
                NwSelectedLevel = SelectedCharacter.NWLevel;
            }
            else
            {
                NwSelectedType = PanelType.None;
            }

            var nePanel = Panels.FirstOrDefault(p => p.EnglishName.Equals(SelectedCharacter.NEPanel));
            if (nePanel != null)
            {
                NeSelectedType = nePanel.PanelType;
                NeSelectedPanel = nePanel;
                NeSelectedLevel = SelectedCharacter.NELevel;
            }
            else
            {
                NeSelectedType = PanelType.None;
            }

            var wPanel = Panels.FirstOrDefault(p => p.EnglishName.Equals(SelectedCharacter.WPanel));
            if (wPanel != null)
            {
                WSelectedType = wPanel.PanelType;
                WSelectedPanel = wPanel;
                WSelectedLevel = SelectedCharacter.WLevel;
            }
            else
            {
                WSelectedType = PanelType.None;
            }

            var cPanel = Panels.FirstOrDefault(p => p.EnglishName.Equals(SelectedCharacter.CPanel));
            if (cPanel != null)
            {
                CSelectedType = cPanel.PanelType;
                CSelectedPanel = cPanel;
                CSelectedLevel = SelectedCharacter.CLevel;
            }
            else
            {
                CSelectedType = PanelType.None;
            }

            var ePanel = Panels.FirstOrDefault(p => p.EnglishName.Equals(SelectedCharacter.EPanel));
            if (ePanel != null)
            {
                ESelectedType = ePanel.PanelType;
                ESelectedPanel = ePanel;
                ESelectedLevel = SelectedCharacter.ELevel;
            }
            else
            {
                ESelectedType = PanelType.None;
            }

            var swPanel = Panels.FirstOrDefault(p => p.EnglishName.Equals(SelectedCharacter.SWPanel));
            if (swPanel != null)
            {
                SwSelectedType = swPanel.PanelType;
                SwSelectedPanel = swPanel;
                SwSelectedLevel = SelectedCharacter.SWLevel;
            }
            else
            {
                SwSelectedType = PanelType.None;
            }

            var sePanel = Panels.FirstOrDefault(p => p.EnglishName.Equals(SelectedCharacter.SEPanel));
            if (sePanel != null)
            {
                SeSelectedType = sePanel.PanelType;
                SeSelectedPanel = sePanel;
                SeSelectedLevel = SelectedCharacter.SELevel;
            }
            else
            {
                SeSelectedType = PanelType.None;
            }
        }

        public void Calculate()
        {
            if (SelectedCharacter == null)
                return;

            var panelBoard = new PanelBoard
            {
                NwPanel = NwSelectedPanel,
                NwLevel = NwSelectedLevel,
                NePanel = NeSelectedPanel,
                NeLevel = NeSelectedLevel,
                WPanel = WSelectedPanel,
                WLevel = WSelectedLevel,
                CPanel = CSelectedPanel,
                CLevel = CSelectedLevel,
                EPanel = ESelectedPanel,
                ELevel = ESelectedLevel,
                SwPanel = SwSelectedPanel,
                SwLevel = SwSelectedLevel,
                SePanel = SeSelectedPanel,
                SeLevel = SeSelectedLevel
            };
            var result = CharacterStatCalculator.CalculateCharacterStats(SelectedCharacter, panelBoard);

            RawStrength = result.RawStrength;
            BonusStrength = result.BonusStrength;
            FinalStrength = RawStrength + BonusStrength;
            RawSkill = result.RawSkill;
            BonusSkill = result.BonusSkill;
            FinalSkill = RawSkill + BonusSkill;
            RawMagic = result.RawMagic;
            BonusMagic = result.BonusMagic;
            FinalMagic = RawMagic + BonusMagic;
            RawEndurance = result.RawEndurance;
            BonusEndurance = result.BonusEndurance;
            FinalEndurance = RawEndurance + BonusEndurance;
            RawSpirit = result.RawSpirit;
            BonusSpirit = result.BonusSpirit;
            FinalSpirit = RawSpirit + BonusSpirit;
            RawFire = result.RawFire;
            BonusFire = result.BonusFire;
            FinalFire = RawFire + BonusFire;
            RawEarth = result.RawEarth;
            BonusEarth = result.BonusEarth;
            FinalEarth = RawEarth + BonusEarth;
            RawMetal = result.RawMetal;
            BonusMetal = result.BonusMetal;
            FinalMetal = RawMetal + BonusMetal;
            RawWater = result.RawWater;
            BonusWater = result.BonusWater;
            FinalWater = RawWater + BonusWater;
            RawWood = result.RawWood;
            BonusWood = result.BonusWood;
            FinalWood = RawWood + BonusWood;
        }

        private void ReloadPanelTypes()
        {
            PanelTypes.Clear();
            PanelTypes.Add(PanelType.None, string.Empty);
            var panelTypes = Enum.GetValues(typeof(PanelType)).Cast<PanelType>();
            foreach (var panelType in panelTypes)
            {
                if (panelType == PanelType.None)
                    continue;
                PanelTypes.Add(panelType, Properties.Resources.ResourceManager.GetString(panelType.ToString()));
            }
        }

        public void UpdateLanguage()
        {
            ReloadPanelTypes();
            View.UpdateLanguage();
        }

        public void Transfer()
        {
            DamageCalculatorController.TransferCharacterData(
                new CharacterData
                { 
                    Strength = FinalStrength,
                    Skill = FinalSkill,
                    Magic = FinalMagic,
                    Endurance = FinalEndurance,
                    Spirit = FinalSpirit,
                    Fire = FinalFire,
                    Water = FinalWater,
                    Earth = FinalEarth,
                    Metal = FinalMetal,
                    Wood = FinalWood,
                    Hp = CharacterHp
                }
            );
            MainController.DisplayDamageCalculatorView();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetMainController(IMainController mainController)
        {
            MainController = mainController;
        }
    }
}
