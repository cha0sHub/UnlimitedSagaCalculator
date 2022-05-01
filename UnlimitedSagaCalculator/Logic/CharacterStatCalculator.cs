using System;
using System.Collections.Generic;
using System.Linq;
using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Logic
{
    internal class CharacterStatCalculator : ICharacterStatCalculator
    {
        public CharacterStatResult CalculateCharacterStats(CharacterData characterData, PanelBoard panelBoard)
        {
            var result = new CharacterStatResult
            {
                RawStrength = CalculateRawStrength(characterData, panelBoard),
                RawSkill = CalculateRawSkill(characterData, panelBoard),
                RawSpirit = CalculateRawSpirit(characterData, panelBoard),
                RawMagic = CalculateRawMagic(characterData, panelBoard),
                RawEndurance = CalculateRawEndurance(characterData, panelBoard),
                RawFire = CalculateRawElement(characterData, panelBoard, "Fire"),
                RawEarth = CalculateRawElement(characterData, panelBoard, "Earth"),
                RawMetal = CalculateRawElement(characterData, panelBoard, "Metal"),
                RawWater = CalculateRawElement(characterData, panelBoard, "Water"),
                RawWood = CalculateRawElement(characterData, panelBoard, "Wood")
            };

            CalculateBonuses(result, characterData, panelBoard);

            return result;
        }

        private void CalculateBonuses(CharacterStatResult result, CharacterData characterData, PanelBoard panelBoard)
        {
            var bonusResults = new List<BonusResult>();

            if (characterData.PrioritizeLine)
            {
                bonusResults.AddRange(GetLineBonuses(characterData, panelBoard));
                bonusResults.AddRange(GetTriangleBonuses(panelBoard));
            }
            else
            {
                bonusResults.AddRange(GetTriangleBonuses(panelBoard));
                bonusResults.AddRange(GetLineBonuses(characterData, panelBoard));
            }

            bonusResults.AddRange(GetJointBonuses(characterData, panelBoard));

            result.BonusStrength = bonusResults.Sum(b => b.BonusStrength);
            result.BonusSkill = bonusResults.Sum(b => b.BonusSkill);
            result.BonusSpirit = bonusResults.Sum(b => b.BonusSpirit);
            result.BonusMagic = bonusResults.Sum(b => b.BonusMagic);
            result.BonusEndurance = bonusResults.Sum(b => b.BonusEndurance);

            result.BonusFire = bonusResults.Sum(b => b.BonusFire);
            result.BonusEarth = bonusResults.Sum(b => b.BonusEarth);
            result.BonusMetal = bonusResults.Sum(b => b.BonusMetal);
            result.BonusWater = bonusResults.Sum(b => b.BonusWater);
            result.BonusWood = bonusResults.Sum(b => b.BonusWood);

            result.LineBonuses = bonusResults.Where(b => b.BonusType == BonusType.Line).Select(b => b.BonusSlot).ToList();
            result.TriangleBonuses = bonusResults.Where(b => b.BonusType == BonusType.Triangle).Select(b => b.BonusSlot).ToList();
            result.JointBonuses = bonusResults.Where(b => b.BonusType == BonusType.Joint).Select(b => b.BonusSlot).ToList();
        }

        public List<BonusResult> GetTriangleBonuses(PanelBoard panelBoard)
        {
            var bonuses = new List<BonusResult>();

            if (!panelBoard.NwUsedForBonus && !panelBoard.EUsedForBonus && !panelBoard.SwUsedForBonus)
            {
                if (PanelTypesMatch(panelBoard.NwPanel, panelBoard.EPanel, panelBoard.SwPanel))
                {
                    var sameLevelBonus = GetSameLevelBonus(panelBoard.NwLevel, panelBoard.ELevel, panelBoard.SwLevel);
                    var bonusResult = GetBonusResult(panelBoard.NwPanel, BonusType.Triangle, sameLevelBonus);
                    bonusResult.BonusType = BonusType.Triangle;
                    bonusResult.BonusSlot = 1;
                    panelBoard.NwUsedForBonus = true;
                    panelBoard.EUsedForBonus = true;
                    panelBoard.SwUsedForBonus = true;
                    bonuses.Add(bonusResult);
                }
            }
            if (!panelBoard.NeUsedForBonus && !panelBoard.WUsedForBonus && !panelBoard.SeUsedForBonus)
            {
                if (PanelTypesMatch(panelBoard.NePanel, panelBoard.WPanel, panelBoard.SePanel))
                {
                    var sameLevelBonus = GetSameLevelBonus(panelBoard.NeLevel, panelBoard.WLevel, panelBoard.SeLevel);
                    var bonusResult = GetBonusResult(panelBoard.NePanel, BonusType.Triangle, sameLevelBonus);
                    bonusResult.BonusType = BonusType.Triangle;
                    bonusResult.BonusSlot = 2;
                    panelBoard.NeUsedForBonus = true;
                    panelBoard.WUsedForBonus = true;
                    panelBoard.SeUsedForBonus = true;
                    bonuses.Add(bonusResult);
                }
            }

            return bonuses;
        }

        public List<BonusResult> GetLineBonuses(CharacterData characterData, PanelBoard panelBoard)
        {
            var bonuses = new List<BonusResult>();

            foreach (var linePriority in characterData.LinePriorities)
            {
                var bonusResult = GetLineBonus(panelBoard, linePriority);
                if (bonusResult != null)
                    bonuses.Add(bonusResult);
            }

            return bonuses;
        }

        public BonusResult GetLineBonus(PanelBoard panelBoard, int bonusSlot)
        {
            BonusResult bonusResult = null;

            if (bonusSlot == 1)
            {
                if (!panelBoard.NwUsedForBonus && !panelBoard.CUsedForBonus && !panelBoard.SeUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.NwPanel, panelBoard.CPanel, panelBoard.SePanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.NwLevel, panelBoard.CLevel, panelBoard.SeLevel);
                        bonusResult = GetBonusResult(panelBoard.NwPanel, BonusType.Line, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Line;
                        bonusResult.BonusSlot = 1;
                        panelBoard.NwUsedForBonus = true;
                        panelBoard.CUsedForBonus = true;
                        panelBoard.SeUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 2)
            {
                if (!panelBoard.NeUsedForBonus && !panelBoard.CUsedForBonus && !panelBoard.SwUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.NePanel, panelBoard.CPanel, panelBoard.SwPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.NeLevel, panelBoard.CLevel, panelBoard.SwLevel);
                        bonusResult = GetBonusResult(panelBoard.NePanel, BonusType.Line, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Line;
                        bonusResult.BonusSlot = 2;
                        panelBoard.NeUsedForBonus = true;
                        panelBoard.CUsedForBonus = true;
                        panelBoard.SwUsedForBonus = true;
                    }
                }
            }
            else
            {
                if (!panelBoard.WUsedForBonus && !panelBoard.CUsedForBonus && !panelBoard.EUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.WPanel, panelBoard.CPanel, panelBoard.EPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.WLevel, panelBoard.CLevel, panelBoard.ELevel);
                        bonusResult = GetBonusResult(panelBoard.WPanel, BonusType.Line, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Line;
                        bonusResult.BonusSlot = 3;
                        panelBoard.WUsedForBonus = true;
                        panelBoard.CUsedForBonus = true;
                        panelBoard.EUsedForBonus = true;
                    }
                }
            }

            return bonusResult;
        }

        public List<BonusResult> GetJointBonuses(CharacterData characterData, PanelBoard panelBoard)
        {
            var bonuses = new List<BonusResult>();

            foreach (var jointPriority in characterData.JointPriorities)
            {
                var bonusResult = GetJointBonus(panelBoard, jointPriority);
                if (bonusResult != null)
                    bonuses.Add(bonusResult);
            }
            foreach (var jointPriority in characterData.JointPriorities)
            {
                var bonusResult = GetJointBonus(panelBoard, jointPriority + 6);
                if (bonusResult != null)
                    bonuses.Add(bonusResult);
            }

            return bonuses;
        }

        public BonusResult GetJointBonus(PanelBoard panelBoard, int bonusSlot)
        {
            BonusResult bonusResult = null;

            if (bonusSlot == 1)
            {
                if (!panelBoard.NwUsedForBonus && !panelBoard.NeUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.NwPanel, panelBoard.NePanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.NwLevel, panelBoard.NeLevel);
                        bonusResult = GetBonusResult(panelBoard.NwPanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 1;
                        panelBoard.NwUsedForBonus = true;
                        panelBoard.NeUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 2)
            {
                if (!panelBoard.NeUsedForBonus && !panelBoard.EUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.NePanel, panelBoard.EPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.NeLevel, panelBoard.ELevel);
                        bonusResult = GetBonusResult(panelBoard.NePanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 2;
                        panelBoard.NeUsedForBonus = true;
                        panelBoard.WUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 3)
            {
                if (!panelBoard.EUsedForBonus && !panelBoard.SeUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.EPanel, panelBoard.SePanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.ELevel, panelBoard.SeLevel);
                        bonusResult = GetBonusResult(panelBoard.EPanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 3;
                        panelBoard.EUsedForBonus = true;
                        panelBoard.SeUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 4)
            {
                if (!panelBoard.SeUsedForBonus && !panelBoard.SwUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.SePanel, panelBoard.SwPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.SeLevel, panelBoard.SwLevel);
                        bonusResult = GetBonusResult(panelBoard.SePanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 4;
                        panelBoard.SeUsedForBonus = true;
                        panelBoard.SwUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 5)
            {
                if (!panelBoard.SwUsedForBonus && !panelBoard.WUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.SwPanel, panelBoard.WPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.SwLevel, panelBoard.WLevel);
                        bonusResult = GetBonusResult(panelBoard.SwPanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 5;
                        panelBoard.SwUsedForBonus = true;
                        panelBoard.WUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 6)
            {
                if (!panelBoard.WUsedForBonus && !panelBoard.NwUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.WPanel, panelBoard.NwPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.WLevel, panelBoard.NwLevel);
                        bonusResult = GetBonusResult(panelBoard.WPanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 6;
                        panelBoard.WUsedForBonus = true;
                        panelBoard.NwUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 7)
            {
                if (!panelBoard.NwUsedForBonus && !panelBoard.CUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.NwPanel, panelBoard.CPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.NwLevel, panelBoard.CLevel);
                        bonusResult = GetBonusResult(panelBoard.NwPanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 7;
                        panelBoard.NwUsedForBonus = true;
                        panelBoard.CUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 8)
            {
                if (!panelBoard.NeUsedForBonus && !panelBoard.CUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.NePanel, panelBoard.CPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.NeLevel, panelBoard.CLevel);
                        bonusResult = GetBonusResult(panelBoard.NePanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 8;
                        panelBoard.NeUsedForBonus = true;
                        panelBoard.CUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 9)
            {
                if (!panelBoard.EUsedForBonus && !panelBoard.CUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.EPanel, panelBoard.CPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.ELevel, panelBoard.CLevel);
                        bonusResult = GetBonusResult(panelBoard.EPanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 9;
                        panelBoard.EUsedForBonus = true;
                        panelBoard.CUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 10)
            {
                if (!panelBoard.SeUsedForBonus && !panelBoard.CUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.SePanel, panelBoard.CPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.SeLevel, panelBoard.CLevel);
                        bonusResult = GetBonusResult(panelBoard.SePanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 10;
                        panelBoard.SeUsedForBonus = true;
                        panelBoard.CUsedForBonus = true;
                    }
                }
            }
            else if (bonusSlot == 11)
            {
                if (!panelBoard.SwUsedForBonus && !panelBoard.CUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.SwPanel, panelBoard.CPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.SwLevel, panelBoard.CLevel);
                        bonusResult = GetBonusResult(panelBoard.SwPanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 11;
                        panelBoard.SwUsedForBonus = true;
                        panelBoard.CUsedForBonus = true;
                    }
                }
            }
            else
            {
                if (!panelBoard.WUsedForBonus && !panelBoard.CUsedForBonus)
                {
                    if (PanelTypesMatch(panelBoard.WPanel, panelBoard.CPanel))
                    {
                        var sameLevelBonus = GetSameLevelBonus(panelBoard.WLevel, panelBoard.CLevel);
                        bonusResult = GetBonusResult(panelBoard.WPanel, BonusType.Joint, sameLevelBonus);
                        bonusResult.BonusType = BonusType.Joint;
                        bonusResult.BonusSlot = 12;
                        panelBoard.WUsedForBonus = true;
                        panelBoard.CUsedForBonus = true;
                    }
                }
            }

            return bonusResult;
        }

        public BonusResult GetBonusResult(PanelData panelData, BonusType bonusType, int sameLevelBonus)
        {
            var bonusResult = new BonusResult
            {
                BonusStrength = GetBonusValue(bonusType, panelData.StrengthBonus, sameLevelBonus),
                BonusSkill = GetBonusValue(bonusType, panelData.SkillBonus, sameLevelBonus),
                BonusSpirit = GetBonusValue(bonusType, panelData.SpiritBonus, sameLevelBonus),
                BonusMagic = GetBonusValue(bonusType, panelData.MagicBonus, sameLevelBonus),
                BonusEndurance = GetBonusValue(bonusType, panelData.EnduranceBonus, sameLevelBonus),
                BonusFire = GetBonusValue(bonusType, panelData.FireBonus, sameLevelBonus),
                BonusEarth = GetBonusValue(bonusType, panelData.EarthBonus, sameLevelBonus),
                BonusMetal = GetBonusValue(bonusType, panelData.MetalBonus, sameLevelBonus),
                BonusWater = GetBonusValue(bonusType, panelData.WaterBonus, sameLevelBonus),
                BonusWood = GetBonusValue(bonusType, panelData.WoodBonus, sameLevelBonus)
            };

            return bonusResult;
        }

        public int GetBonusValue(BonusType bonusType, double bonus, int sameLevelBonus)
        {
            if (bonusType == BonusType.Triangle && bonus == 4)
                bonus = 4.5;
            var bonusMultiplier = 1;
            if (bonusType == BonusType.Triangle)
                bonusMultiplier = 2;
            if (bonusType == BonusType.Line)
                bonusMultiplier = 3;
            var bonusValue = bonus * bonusMultiplier;
            if (bonusValue > 0)
                bonusValue += sameLevelBonus;
            return (int)bonusValue;
        }

        public bool PanelTypesMatch(params PanelData[] panels)
        {
            if (panels.Length == 0)
                return false;
            if (panels.Any(p => p == null))
                return false;

            var expectedType = panels[0].PanelType;

            foreach (var panel in panels) 
            {
                if (panel.PanelType != expectedType)
                    return false;
            }

            return true;
        }

        public int GetSameLevelBonus(params int[] panelLevels)
        {
            if (panelLevels.Length == 0)
                return 0;

            var expectedLevel = panelLevels[0];

            foreach (var level in panelLevels)
            {
                if (expectedLevel != level)
                    return 0;
            }

            return expectedLevel;
        }

        private int CalculateRawStrength(CharacterData characterData, PanelBoard panelBoard)
        {
            var nwStrengthContribution = panelBoard.NwPanel == null ? 0 : GetPanelContribution(panelBoard.NwPanel.AbilityGrowth, panelBoard.NwLevel, characterData.Strength, .6);
            var neStrengthContribution = panelBoard.NePanel == null ? 0 : GetPanelContribution(panelBoard.NePanel.AbilityGrowth, panelBoard.NeLevel, characterData.Strength, .6);
            var cStrengthContribution = panelBoard.CPanel == null ? 0 : GetPanelContribution(panelBoard.CPanel.AbilityGrowth, panelBoard.CLevel, characterData.Strength, .2);
            return (int)Math.Floor(nwStrengthContribution + neStrengthContribution + cStrengthContribution + characterData.Strength);
        }

        private int CalculateRawSkill(CharacterData characterData, PanelBoard panelBoard)
        {
            var neSkillContribution = panelBoard.NePanel == null ? 0 : GetPanelContribution(panelBoard.NePanel.AbilityGrowth, panelBoard.NeLevel, characterData.Skill, .4);
            var eSkillContribution = panelBoard.EPanel == null ? 0 : GetPanelContribution(panelBoard.EPanel.AbilityGrowth, panelBoard.ELevel, characterData.Skill, .8);
            var cSkillContribution = panelBoard.CPanel == null ? 0 : GetPanelContribution(panelBoard.CPanel.AbilityGrowth, panelBoard.CLevel, characterData.Skill, .2);
            return (int)Math.Floor(neSkillContribution + cSkillContribution + eSkillContribution + characterData.Skill);
        }

        private int CalculateRawSpirit(CharacterData characterData, PanelBoard panelBoard)
        {
            var cSpiritContribution = panelBoard.NePanel == null ? 0 : GetPanelContribution(panelBoard.NePanel.AbilityGrowth, panelBoard.CLevel, characterData.Spirit, .2);
            var eSpiritContribution = panelBoard.EPanel == null ? 0 : GetPanelContribution(panelBoard.EPanel.AbilityGrowth, panelBoard.ELevel, characterData.Spirit, .2);
            var seSpiritContribution = panelBoard.CPanel == null ? 0 : GetPanelContribution(panelBoard.CPanel.AbilityGrowth, panelBoard.SeLevel, characterData.Spirit, 1.0);
            return (int)Math.Floor(cSpiritContribution + eSpiritContribution + seSpiritContribution + characterData.Spirit);
        }

        private int CalculateRawMagic(CharacterData characterData, PanelBoard panelBoard)
        {
            var wMagicContribution = panelBoard.WPanel == null ? 0 : GetPanelContribution(panelBoard.WPanel.AbilityGrowth, panelBoard.WLevel, characterData.Magic, .2);
            var cMagicContribution = panelBoard.CPanel == null ? 0 : GetPanelContribution(panelBoard.CPanel.AbilityGrowth, panelBoard.CLevel, characterData.Magic, .2);
            var swMagicContribution = panelBoard.SwPanel == null ? 0 : GetPanelContribution(panelBoard.SwPanel.AbilityGrowth, panelBoard.SwLevel, characterData.Magic, 1.0);
            return (int)Math.Floor(wMagicContribution + cMagicContribution + swMagicContribution + characterData.Magic);
        }

        private int CalculateRawEndurance(CharacterData characterData, PanelBoard panelBoard)
        {
            var nwEnduranceContribution = panelBoard.NwPanel == null ? 0 : GetPanelContribution(panelBoard.NwPanel.AbilityGrowth, panelBoard.NwLevel, characterData.Endurance, .4);
            var wEnduranceContribution = panelBoard.WPanel == null ? 0 : GetPanelContribution(panelBoard.WPanel.AbilityGrowth, panelBoard.WLevel, characterData.Endurance, .8);
            var cEnduranceContribution = panelBoard.CPanel == null ? 0 : GetPanelContribution(panelBoard.CPanel.AbilityGrowth, panelBoard.CLevel, characterData.Endurance, 0.2);
            return (int)Math.Floor(nwEnduranceContribution + wEnduranceContribution + cEnduranceContribution + characterData.Endurance);
        }

        private int CalculateRawElement(CharacterData characterData, PanelBoard panelBoard, string elementName)
        {
            var elementIndex = GetCharacterElementIndex(characterData, elementName);
            if (elementIndex == 1)
                return CalculateFirstElement(characterData, panelBoard);
            else if (elementIndex == 2)
                return CalculateSecondElement(characterData, panelBoard);
            else if (elementIndex == 3)
                return CalculateThirdElement(characterData, panelBoard);
            else if (elementIndex == 4)
                return CalculateFourthElement(characterData, panelBoard);
            else
                return CalculateFifthElement(characterData, panelBoard);
        }

        private int CalculateFirstElement(CharacterData characterData, PanelBoard panelBoard)
        {
            var characterElementStat = GetCharacterElementStat(characterData, 1);
            var nwContribution = panelBoard.NwPanel == null ? 0 : GetPanelContribution(panelBoard.NwPanel.ElementGrowth, panelBoard.NwLevel, characterElementStat, 1.0);
            var wContribution = panelBoard.WPanel == null ? 0 : GetPanelContribution(panelBoard.WPanel.ElementGrowth, panelBoard.WLevel, characterElementStat, .2);
            var cContribution = panelBoard.CPanel == null ? 0 : GetPanelContribution(panelBoard.CPanel.ElementGrowth, panelBoard.CLevel, characterElementStat, 0.2);
            return (int)Math.Floor(nwContribution + wContribution + cContribution + characterElementStat);
        }

        private int CalculateSecondElement(CharacterData characterData, PanelBoard panelBoard)
        {
            var characterElementStat = GetCharacterElementStat(characterData, 2);
            var neContribution = panelBoard.NePanel == null ? 0 : GetPanelContribution(panelBoard.NePanel.ElementGrowth, panelBoard.NeLevel, characterElementStat, 1.0);
            var cContribution = panelBoard.CPanel == null ? 0 : GetPanelContribution(panelBoard.CPanel.ElementGrowth, panelBoard.CLevel, characterElementStat, .2);
            var eContribution = panelBoard.EPanel == null ? 0 : GetPanelContribution(panelBoard.EPanel.ElementGrowth, panelBoard.ELevel, characterElementStat, 0.2);
            return (int)Math.Floor(neContribution + cContribution + eContribution + characterElementStat);
        }

        private int CalculateThirdElement(CharacterData characterData, PanelBoard panelBoard)
        {
            var characterElementStat = GetCharacterElementStat(characterData, 3);
            var cContribution = panelBoard.CPanel == null ? 0 : GetPanelContribution(panelBoard.CPanel.ElementGrowth, panelBoard.CLevel, characterElementStat, 0.2);
            var eContribution = panelBoard.EPanel == null ? 0 : GetPanelContribution(panelBoard.EPanel.ElementGrowth, panelBoard.ELevel, characterElementStat, .8);
            var seContribution = panelBoard.SePanel == null ? 0 : GetPanelContribution(panelBoard.SePanel.ElementGrowth, panelBoard.SeLevel, characterElementStat, 0.4);
            return (int)Math.Floor(cContribution + eContribution + seContribution + characterElementStat);
        }

        private int CalculateFourthElement(CharacterData characterData, PanelBoard panelBoard)
        {
            var characterElementStat = GetCharacterElementStat(characterData, 4);
            var cContribution = panelBoard.CPanel == null ? 0 : GetPanelContribution(panelBoard.CPanel.ElementGrowth, panelBoard.CLevel, characterElementStat, .2);
            var swContribution = panelBoard.SwPanel == null ? 0 : GetPanelContribution(panelBoard.SwPanel.ElementGrowth, panelBoard.SwLevel, characterElementStat, .6);
            var seContribution = panelBoard.SePanel == null ? 0 : GetPanelContribution(panelBoard.SePanel.ElementGrowth, panelBoard.SeLevel, characterElementStat, 0.6);
            return (int)Math.Floor(cContribution + swContribution + seContribution + characterElementStat);
        }

        private int CalculateFifthElement(CharacterData characterData, PanelBoard panelBoard)
        {
            var characterElementStat = GetCharacterElementStat(characterData, 5);
            var wContribution = panelBoard.WPanel == null ? 0 : GetPanelContribution(panelBoard.WPanel.ElementGrowth, panelBoard.WLevel, characterElementStat, .8);
            var cContribution = panelBoard.CPanel == null ? 0 : GetPanelContribution(panelBoard.CPanel.ElementGrowth, panelBoard.CLevel, characterElementStat, .2);
            var swContribution = panelBoard.SwPanel == null ? 0 : GetPanelContribution(panelBoard.SwPanel.ElementGrowth, panelBoard.SwLevel, characterElementStat, 0.4);
            return (int)Math.Floor(wContribution + cContribution + swContribution + characterElementStat);
        }

        private double GetCharacterElementStat(CharacterData characterData, int element)
        {
            var characterElement = "";
            if (element == 1)
                characterElement = characterData.FirstElement;
            if (element == 2)
                characterElement = characterData.SecondElement;
            if (element == 3)
                characterElement = characterData.ThirdElement;
            if (element == 4)
                characterElement = characterData.FourthElement;
            if (element == 5)
                characterElement = characterData.FifthElement;

            if (characterElement == "Fire")
                return characterData.Fire;
            else if (characterElement == "Earth")
                return characterData.Earth;
            else if (characterElement == "Metal")
                return characterData.Metal;
            else if (characterElement == "Water")
                return characterData.Water;
            else
                return characterData.Wood;
        }

        private double GetCharacterElementIndex(CharacterData characterData, string elementName)
        {
            if (characterData.FirstElement == elementName)
                return 1;
            else if (characterData.SecondElement == elementName)
                return 2;
            else if (characterData.ThirdElement == elementName)
                return 3;
            else if (characterData.FourthElement == elementName)
                return 4;
            else
                return 5;
        }

        private double GetPanelContribution(double panelScore, double panelLevel, double characterAffinity, double contributionPercentage)
        {
            var baseGrowthValue = panelScore * panelLevel;

            var panelGrowthValue = baseGrowthValue * contributionPercentage;

            var affinityMultiplier = characterAffinity + 3;

            var contribution = panelGrowthValue * affinityMultiplier / 64;

            return contribution;
        }
    }
}
