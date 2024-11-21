using OF.Base.Client;
using OliverFida.FSimMan.Config;

namespace OliverFida.FSimMan.Client
{
    public class AppSettingsClient : ClientBase
    {
        private const string FILE_NAME = "appSettings.xml";

        public static AppSettings GetSettings()
        {
            AppSettingsData data = ConfigFileClient.DeserializeFile<AppSettingsData>(FILE_NAME);
            AppSettings settings = data.FromData();
            if (!ReleaseFeatures.ApplicationModeCreator) settings.ApplicationModeValues = settings.ApplicationModeValues.Where(x => !x.Equals(ApplicationMode.Creator)).ToList();
            StoreSettings(settings);
            return settings;
        }

        public static void StoreSettings(AppSettings settings)
        {
            AppSettingsData data = new AppSettingsData();
            data.ToData(settings);
            ConfigFileClient.SerializeFile<AppSettingsData>(FILE_NAME, data);
        }
    }
}
