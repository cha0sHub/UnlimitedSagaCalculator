using System.Threading;

namespace UnlimitedSagaCalculator.Model
{
    public class MaterialData
    {
        public string EnglishName { get; set; }
        public string JapaneseName { get; set; }
        public int Weight { get; set; }
        public double BaseValue { get; set; }
        public int DaggerDamage { get; set; }
        public int SwordDamage { get; set; }
        public int AxeDamage { get; set; }
        public int StaffDamage { get; set; }
        public int SpearDamage { get; set; }
        public int BowDamage { get; set; }
        public int GunDamage
        { get; set; }

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
