using System.Threading;

namespace UnlimitedSagaCalculator.Model
{
    public class AttackData
    {
        public string EnglishName { get; set; }
        public string JapaneseName { get; set; }
        public AttackType AttackType { get; set; }
        public WeaponType WeaponType { get; set; }
        public MagicType MagicType { get; set; }
        public double EnCost { get; set; }
        public double GrowthRate { get; set; }
        public double HpDamage { get; set; }
        public double LpDamage { get; set; }
        public double NumberOfHits { get; set; }
        public AttackAttribute[] Attributes { get; set; }
        public bool IsCommon { get; set; }

        public override string ToString()
        {
            if (Thread.CurrentThread.CurrentUICulture.Name == "ja-JP")
            {
                return JapaneseName;
            }
            return EnglishName;
        }
    }
}
