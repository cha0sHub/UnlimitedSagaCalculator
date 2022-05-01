namespace UnlimitedSagaCalculator.Logic
{
    public class BonusResult
    {
        public int BonusStrength { get; set; }
        public int BonusSkill { get; set; }
        public int BonusSpirit { get; set; }
        public int BonusMagic {get; set; }
        public int BonusEndurance { get; set; }
        public int BonusFire { get; set; }
        public int BonusEarth { get; set; }
        public int BonusMetal { get; set; }
        public int BonusWater { get; set; }
        public int BonusWood { get; set; }
        public BonusType BonusType { get; set; }
        public int BonusSlot { get; set; }
    }

    public enum BonusType
    {
        Line,
        Triangle,
        Joint
    }
}
