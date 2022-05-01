using Newtonsoft.Json;
using System.IO;

namespace UnlimitedSagaCalculator.Model
{
    internal class UsagaModel : IUsagaModel
    {
        private const string AttackPath = "Model/Attacks.json";
        private const string EnemyPath = "Model/Enemies.json";
        private const string CharacterPath = "Model/Characters.json";
        private const string PanelPath = "Model/Panels.json";
        private const string MaterialPath = "Model/Materials.json";
        private const string EquipmentTypePath = "Model/EquipmentTypes.json";

        public AttackCollection AttackCollection { get; private set; }
        public EnemyCollection EnemyCollection { get; private set; }
        public CharacterCollection CharacterCollection { get; private set; }
        public PanelCollection PanelCollection { get; private set; }
        public MaterialCollection MaterialCollection { get; private set; }
        public EquipmentTypeCollection EquipmentTypeCollection { get; private set; }

        public UsagaModel()
        {
            ParseData();
        }

        private void ParseData()
        {
            var attackData = string.Empty;
            using (var attackReader = File.OpenText(AttackPath))
            {
                attackData = attackReader.ReadToEnd();
            }
            AttackCollection = JsonConvert.DeserializeObject<AttackCollection>(attackData);

            var enemyData = string.Empty;
            using (var enemyReader = File.OpenText(EnemyPath))
            { 
                enemyData = enemyReader.ReadToEnd();
            }
            EnemyCollection = JsonConvert.DeserializeObject<EnemyCollection>(enemyData);

            var characterData = string.Empty;
            using (var characterReader = File.OpenText(CharacterPath))
            {
                characterData = characterReader.ReadToEnd();
            }
            CharacterCollection = JsonConvert.DeserializeObject<CharacterCollection>(characterData);

            var panelData = string.Empty;
            using (var panelReader = File.OpenText(PanelPath))
            {
                panelData = panelReader.ReadToEnd();
            }
            PanelCollection = JsonConvert.DeserializeObject<PanelCollection>(panelData);

            var materialData = string.Empty;
            using (var materialReader = File.OpenText(MaterialPath))
            {
                materialData = materialReader.ReadToEnd();
            }
            MaterialCollection = JsonConvert.DeserializeObject<MaterialCollection>(materialData);

            var equipmentTypeData = string.Empty;
            using (var equipmentTypeReader = File.OpenText(EquipmentTypePath))
            {
                equipmentTypeData = equipmentTypeReader.ReadToEnd();
            }
            EquipmentTypeCollection = JsonConvert.DeserializeObject<EquipmentTypeCollection>(equipmentTypeData);
        }
    }
}
