using MathNet.Numerics.Distributions;
using System;
using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Logic
{
    internal class LpDamageCalculator : ILpDamageCalculator
    {
        public LpDamageCalculator()
        { 
        
        }

        public LpDamageResult CalculateLpDamageResult(AttackData attackData, EnemyData enemyData, double attackHpDamage, double comboPercentage)
        {
            var damageProbability = Math.Max(0,((attackData.LpDamage * 8 + 20) * (enemyData.Hp - enemyData.CurrentHp + 1) / enemyData.Hp * comboPercentage - enemyData.LpDefense * 5) / 70);

            var numberOfTries = (int)attackData.NumberOfHits;
            if (attackHpDamage > 499)
                numberOfTries++;
            if (attackHpDamage > 999)
                numberOfTries++;
            if (attackHpDamage > enemyData.Hp)
                numberOfTries++;

            damageProbability = Math.Min(1, damageProbability);

            var binomial = new Binomial(damageProbability, numberOfTries);

            var oddsOfNoDamage = binomial.Probability(0);
            var oddsOfOneDamage = binomial.Probability(1);
            var oddsOfTwoDamage = binomial.Probability(2);
            var oddsOfThreeDamage = 1.0 - oddsOfNoDamage - oddsOfOneDamage - oddsOfTwoDamage;


            return new LpDamageResult
            {
                DamageProbability = damageProbability,
                OddsOfNoDamage = oddsOfNoDamage < 0 ? 0 : oddsOfNoDamage,
                OddsOfOneDamage = oddsOfOneDamage < 0 ? 0 : oddsOfOneDamage,
                OddsOfTwoDamage = oddsOfTwoDamage < 0 ? 0 : oddsOfTwoDamage,
                OddsOfThreeDamage = oddsOfThreeDamage < 0 ? 0 : oddsOfThreeDamage
            };
        }

    }
}
