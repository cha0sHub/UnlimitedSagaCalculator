using Newtonsoft.Json;
using System.IO;

namespace UnlimitedSagaCalculator.Configuration
{
    internal class ConfigurationManager : IConfigurationManager
    {
        private const string UserSettingsPath = "settings.json";
        public UserSettings UserSettings { get; private set; }

        public ConfigurationManager()
        {
            UserSettings = new UserSettings();
        }

        public void LoadConfiguration()
        {
            if (!File.Exists(UserSettingsPath))
                return;
            var settingsData = string.Empty;
            using (var settingsReader = File.OpenText(UserSettingsPath))
            {
                settingsData = settingsReader.ReadToEnd();
            }
            UserSettings = JsonConvert.DeserializeObject<UserSettings>(settingsData);
        }

        public void SaveConfiguration()
        {
            var settingsData = JsonConvert.SerializeObject(UserSettings);
            using (var settingsWriter = File.CreateText(UserSettingsPath))
            {
                settingsWriter.Write(settingsData);
            }
        }
    }
}
