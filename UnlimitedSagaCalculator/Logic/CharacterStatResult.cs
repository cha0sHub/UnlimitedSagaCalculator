using System.Collections.Generic;

namespace UnlimitedSagaCalculator.Logic
{
    public class CharacterStatResult
    {
        public int RawStrength { get; set; }
        public int BonusStrength { get; set; }
        public int RawSkill { get; set; }
        public int BonusSkill { get; set; }
        public int RawMagic { get; set; }
        public int BonusMagic { get; set; }
        public int RawEndurance { get; set; }
        public int BonusEndurance { get; set; }
        public int RawSpirit { get; set; }
        public int BonusSpirit { get; set; }
        public int RawFire { get; set; }
        public int BonusFire { get; set; }
        public int RawEarth { get; set; }
        public int BonusEarth { get; set; }
        public int RawMetal { get; set; }
        public int BonusMetal { get; set; }
        public int RawWater { get; set; }
        public int BonusWater { get; set; }
        public int RawWood { get; set; }
        public int BonusWood { get; set; }
        public List<int> LineBonuses { get; set; } = new List<int>();
        public List<int> TriangleBonuses { get; set; } = new List<int>();
        public List<int> JointBonuses { get; set; } = new List<int>();
    }
}
