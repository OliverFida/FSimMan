using OF.FSimMan.Management;
using OF.FSimMan.Management.Games.Fs;
using OF.FSimMan.Utility;

namespace OF.FSimMan.Tests.Dev
{
    [TestClass]
    public class FSimManSettings
    {
        [TestMethod]
        public void Serialization()
        {
            AppSettings appSettings = GetAppSettings();

            AppSettingsData data = new AppSettingsData();
            data.ToData(appSettings);

            FileSerializationHelper.SerializeFile(@"C:\Users\Oliver Fida\Desktop\temp.xml", data);
        }

        [TestMethod]
        public void Deserialization()
        {
            AppSettingsData data = FileSerializationHelper.DeserializeFile<AppSettingsData>(@"C:\Users\Oliver Fida\Desktop\temp.xml");
            AppSettings temp = data.FromData();
        }

        private AppSettings GetAppSettings()
        {
            AppSettings appSettings = new AppSettings
            {
                ApplicationMode = ApplicationMode.Creator
            };

            appSettings.Games.Add(new AppSettingsGameFs22
            {
                IsEnabled = true,
                ExeDirectoryPath = @"B:\Games\Giants\Farming Simulator 2022",
                DataDirectoryPath = @"C:\Users\Oliver Fida\Documents\My Games\FarmingSimulator2022",
            });

            appSettings.Games.Add(new AppSettingsGameFs25
            {
                IsEnabled = false,
                ExeDirectoryPath = @"B:\Games\Giants\Farming Simulator 2025",
                DataDirectoryPath = @"C:\Users\Oliver Fida\Documents\My Games\FarmingSimulator2025",
            });

            return appSettings;
        }
    }
}
