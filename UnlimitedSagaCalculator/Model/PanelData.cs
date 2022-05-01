using System.Threading;

namespace UnlimitedSagaCalculator.Model
{
    public class PanelData
    {
        public string EnglishName { get; set; }
        public string JapaneseName { get; set; }
        public PanelType PanelType { get; set; }
        public double AbilityGrowth { get; set; }
        public double ElementGrowth { get; set; }
        public double StrengthBonus { get; set; }
        public double SkillBonus { get; set; }
        public double SpiritBonus { get; set; }
        public double MagicBonus { get; set; }
        public double EnduranceBonus { get; set; }
        public double FireBonus { get; set; }
        public double EarthBonus { get; set; }
        public double MetalBonus { get; set; }
        public double WaterBonus { get; set; }
        public double WoodBonus { get; set; }

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
