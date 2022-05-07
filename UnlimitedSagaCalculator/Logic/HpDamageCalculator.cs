using System;
using System.Linq;
using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Logic
{
    internal class HpDamageCalculator : IHpDamageCalculator
    {

        public HpDamageCalculator()
        { 
        
        }

        public HpDamageResult CalculateMagicDamage(AttackerData attackerData, EnemyData enemyData, AttackData attackData, double comboPercentage)
        {
            var baseElementDamage = CalculateElementBaseDamage(attackerData, attackData.MagicType);
            var baseMagicDamage = CalculateStatBaseDamage(attackerData.Magic);
            var remainingHpDamage = (Math.Min(150, attackerData.CurrentHp) + 74) / 4;

            var rawDamage = (attackData.HpDamage * 4) + (baseElementDamage * attackData.NumberOfHits) + (baseElementDamage * attackData.GrowthRate * (128 + baseMagicDamage) / 256) + remainingHpDamage;

            var rawDamageUpper = rawDamage + remainingHpDamage;

            return CalculateDamageResult(rawDamage, rawDamageUpper, enemyData, attackData, comboPercentage);
        }

        public HpDamageResult CalculateMartialDamage(AttackerData attackerData, EnemyData enemyData, AttackData attackData, double comboPercentage)
        {
            var baseStrengthDamage = CalculateStatBaseDamage(attackerData.Strength);
            var baseSkillDamage = CalculateStatBaseDamage(attackerData.Skill);

            var rawDamage = attackData.HpDamage * (3 + attackData.EnCost) + (baseStrengthDamage * attackData.NumberOfHits) + (baseStrengthDamage * attackData.GrowthRate) * (128 + baseSkillDamage + attackerData.Weight) / 256;

            var rawDamageUpper = rawDamage + CalculateRand1UpperBound(attackerData.CurrentHp, attackerData.MaxHp);

            return CalculateDamageResult(rawDamage, rawDamageUpper, enemyData, attackData, comboPercentage);
        }

        public HpDamageResult CalculateStrengthDamage(AttackerData attackerData, EnemyData enemyData, AttackData attackData, double comboPercentage)
        {
            var baseStrengthDamage = CalculateStatBaseDamage(attackerData.Strength);

            var rawDamage = attackData.HpDamage * (3 + attackData.EnCost) + (attackerData.WeaponAttackPower * attackData.NumberOfHits) + (baseStrengthDamage * attackData.GrowthRate) * (128 + attackerData.WeaponAttackPower) / 256;

            var rawDamageUpper = rawDamage + CalculateRand2UpperBound(attackerData.CurrentHp, attackerData.MaxHp);

            return CalculateDamageResult(rawDamage, rawDamageUpper, enemyData, attackData, comboPercentage);
        }

        public HpDamageResult CalculateSkillDamage(AttackerData attackerData, EnemyData enemyData, AttackData attackData, double comboPercentage)
        {
            var baseSkillDamage = CalculateStatBaseDamage(attackerData.Skill);

            var rawDamage = attackData.HpDamage * (3 + attackData.EnCost) + (attackerData.WeaponAttackPower * attackData.NumberOfHits) + (baseSkillDamage * attackData.GrowthRate) * (128 + attackerData.WeaponAttackPower) / 256;

            var rawDamageUpper = rawDamage + CalculateRand2UpperBound(attackerData.CurrentHp, attackerData.MaxHp);

            return CalculateDamageResult(rawDamage, rawDamageUpper, enemyData, attackData, comboPercentage);
        }

        private HpDamageResult CalculateDamageResult(double rawDamageLowerBound, double rawDamageUpperBound, EnemyData enemyData, AttackData attackData, double comboPercentage)
        {
            var enemyDefenseValue = GetDefenseValue(enemyData, attackData);

            var finalDamageLowerBound = CalculateFinalDamage(rawDamageLowerBound, enemyDefenseValue, comboPercentage);

            var finalDamageUpperBound = CalculateFinalDamage(rawDamageUpperBound, enemyDefenseValue, comboPercentage);

            return new HpDamageResult
            {
                RawDamageLower = (int)rawDamageLowerBound,
                RawDamageUpper = (int)rawDamageUpperBound,
                FinalDamageLower = (int)finalDamageLowerBound,
                FinalDamageUpper = (int)finalDamageUpperBound
            };
        }

        private double CalculateFinalDamage(double rawDamage, double enemyDefenseValue, double comboPercentage)
        { 
            return (rawDamage * (100 - enemyDefenseValue) / 100 - enemyDefenseValue) * comboPercentage;
        }

        private double GetDefenseValue(EnemyData enemyData, AttackData attackData)
        {
            var defenseValue = 100.0;

            if (attackData.Attributes.Contains(AttackAttribute.Hit) && defenseValue > enemyData.HitDefense)
            {
                defenseValue = enemyData.HitDefense;
            }
            if (attackData.Attributes.Contains(AttackAttribute.Pierce) && defenseValue > enemyData.PierceDefense)
            {
                defenseValue = enemyData.PierceDefense;
            }
            if (attackData.Attributes.Contains(AttackAttribute.Slash) && defenseValue > enemyData.SlashDefense)
            {
                defenseValue = enemyData.SlashDefense;
            }
            if (attackData.Attributes.Contains(AttackAttribute.Lightning) && defenseValue > enemyData.LightningDefense)
            {
                defenseValue = enemyData.LightningDefense;
            }
            if (attackData.Attributes.Contains(AttackAttribute.Light) && defenseValue > enemyData.LightDefense)
            {
                defenseValue = enemyData.LightDefense;
            }
            if (attackData.Attributes.Contains(AttackAttribute.Heat) && defenseValue > enemyData.HeatDefense)
            {
                defenseValue = enemyData.HeatDefense;
            }
            if (attackData.Attributes.Contains(AttackAttribute.Cold) && defenseValue > enemyData.ColdDefense)
            {
                defenseValue = enemyData.ColdDefense;
            }

            return defenseValue;
        }

        private double CalculateRand1UpperBound(double currentHp, double maxHp)
        {
            return 12 * (maxHp - currentHp) / maxHp;
        }

        private double CalculateRand2UpperBound(double currentHp, double maxHp)
        {
            return 12 + 12 * (maxHp - currentHp) / maxHp;
        }

        private double CalculateElementBaseDamage(AttackerData attackerData, MagicType type)
        {
            if (type == MagicType.Fire)
                return CalculateStatBaseDamage(attackerData.Fire);
            if (type == MagicType.Water)
                return CalculateStatBaseDamage(attackerData.Water);
            if (type == MagicType.Wood)
                return CalculateStatBaseDamage(attackerData.Wood);
            if (type == MagicType.Earth)
                return CalculateStatBaseDamage(attackerData.Earth);

            return CalculateStatBaseDamage(attackerData.Metal);
        }

        private double CalculateStatBaseDamage(double statValue)
        {
            return 100 * Math.Pow(statValue, 2) / (Math.Pow(statValue, 2) + 800) + 4;
        }
    }
}
