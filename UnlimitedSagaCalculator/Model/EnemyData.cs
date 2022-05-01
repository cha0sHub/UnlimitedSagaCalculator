using System.Threading;

namespace UnlimitedSagaCalculator.Model
{
    public class EnemyData
    {
        public string EnglishName { get; set; }
        public string JapaneseName { get; set; }
        public EnemyCategory Category { get; set; }
        public double CurrentHp { get; set; }
        public double Hp { get; set; }
        public double Lp { get; set; }
        public double SlashDefense { get; set; }
        public double HitDefense { get; set; }
        public double PierceDefense { get; set; }
        public double HeatDefense { get; set; }
        public double ColdDefense { get; set; }
        public double LightningDefense { get; set; }
        public double LightDefense { get; set; }
        public double LpDefense { get; set; }

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
