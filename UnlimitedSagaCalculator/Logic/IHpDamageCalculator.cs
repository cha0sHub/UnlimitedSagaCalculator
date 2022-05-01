using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Logic
{
    public interface IHpDamageCalculator
    {
        HpDamageResult CalculateMartialDamage(AttackerData attackerData, EnemyData enemyData, AttackData attackData, double comboPercentage);
        HpDamageResult CalculateStrengthDamage(AttackerData attackerData, EnemyData enemyData, AttackData attackData, double comboPercentage);
        HpDamageResult CalculateSkillDamage(AttackerData attackerData, EnemyData enemyData, AttackData attackData, double comboPercentage);
        HpDamageResult CalculateMagicDamage(AttackerData attackerData, EnemyData enemyData, AttackData attackData, double comboPercentage);
    }
}
