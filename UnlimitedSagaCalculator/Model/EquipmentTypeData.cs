using System.Threading;

namespace UnlimitedSagaCalculator.Model
{
    public class EquipmentTypeData
    {
        public string EnglishName { get; set; }
        public string JapaneseName { get; set; }
        public int WeightMultiplier { get; set; }

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
