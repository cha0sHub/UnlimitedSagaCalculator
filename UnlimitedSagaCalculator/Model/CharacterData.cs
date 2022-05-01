using System.Threading;

namespace UnlimitedSagaCalculator.Model
{
    public class CharacterData
    {
        public string EnglishName { get; set; }
        public string JapaneseName { get; set; }
        public int Hp { get; set; }
        public int Lp { get; set; }
        public double Strength { get; set; }
        public double Skill { get; set; }
        public double Magic { get; set; }
        public double Endurance { get; set; }
        public double Spirit { get; set; }
        public double Fire { get; set; }
        public double Earth { get; set; }
        public double Metal { get; set; }
        public double Water { get; set; }
        public double Wood { get; set; }
        public double Weight { get; set; }
        public bool PrioritizeLine { get; set; }
        public int[] LinePriorities { get; set; }
        public int[] JointPriorities { get; set; }
        public string NWPanel { get; set; }
        public int NWLevel { get; set; }
        public string NEPanel { get; set; }
        public int NELevel { get; set; }
        public string WPanel { get; set; }
        public int WLevel { get; set; }
        public string CPanel { get; set; }
        public int CLevel { get; set; }
        public string EPanel { get; set; }
        public int ELevel { get; set; }
        public string SWPanel { get; set; }
        public int SWLevel { get; set; }
        public string SEPanel { get; set; }
        public int SELevel { get; set; }
        public string FirstElement { get; set; }
        public string SecondElement { get; set; }
        public string ThirdElement { get; set; }
        public string FourthElement { get; set; }
        public string FifthElement { get; set; }
        public string WeaponOneMaterial { get; set; }
        public string WeaponOneType { get; set; }
        public string WeaponTwoMaterial { get; set; }
        public string WeaponTwoType { get; set; }
        public string AccessoryOneMaterial { get; set; }
        public string AccessoryOneType { get; set; }
        public string AccessoryTwoMaterial { get; set; }
        public string AccessoryTwoType { get; set; }
        public string HeadMaterial { get; set; }
        public string HeadType { get; set; }
        public string ChestMaterial { get; set; }
        public string ChestType { get; set; }
        public string LegsMaterial { get; set; }
        public string LegsType { get; set; }

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
