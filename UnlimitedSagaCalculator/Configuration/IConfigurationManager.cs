namespace UnlimitedSagaCalculator.Configuration
{
    public interface IConfigurationManager
    {
        UserSettings UserSettings { get; }

        void LoadConfiguration();
        void SaveConfiguration();
    }
}
