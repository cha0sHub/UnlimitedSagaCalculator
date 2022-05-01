namespace UnlimitedSagaCalculator.Model
{
    public interface IUsagaModel
    {
        AttackCollection AttackCollection { get; }
        EnemyCollection EnemyCollection { get; }
        CharacterCollection CharacterCollection { get; }
        PanelCollection PanelCollection { get; }
        MaterialCollection MaterialCollection { get; }
        EquipmentTypeCollection EquipmentTypeCollection { get; }
    }
}
