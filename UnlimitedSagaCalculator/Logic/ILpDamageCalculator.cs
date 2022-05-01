using UnlimitedSagaCalculator.Model;

namespace UnlimitedSagaCalculator.Logic
{
    public interface ILpDamageCalculator
    {
        LpDamageResult CalculateLpDamageResult(AttackData attackData, EnemyData enemyData, double attackHpDamage, double comboPercentage);
    }
}
