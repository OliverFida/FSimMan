using OF.FSimMan.Management;
using OF.FSimMan.Management.Games;
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
            AppSettingsData appSettingsData = GetAppSettingsData();

            AppSettingsData data = new AppSettingsData();
            data.ToData(appSettings);
            Assert.AreEqual(data.ApplicationMode, appSettingsData.ApplicationMode);
            Assert.AreEqual(data.LastSelectedView, appSettingsData.LastSelectedView);
            Assert.AreEqual(data.Games.Count(), appSettingsData.Games.Count());
            for (int i = 0; i < appSettingsData.Games.Count(); i++)
            {
                switch (appSettingsData.Games[i])
                {
                    case AppSettingsGameFs22Data expected:
                        {
                            AppSettingsGameFs22Data actual = data.Games.OfType<AppSettingsGameFs22Data>().Single();
                            Assert.AreEqual(expected.IsEnabled, actual.IsEnabled);
                            Assert.AreEqual(expected.ExeDirectoryPath, actual.ExeDirectoryPath);
                            Assert.AreEqual(expected.DataDirectoryPath, actual.DataDirectoryPath);
                            Assert.AreEqual(expected.StartArguments.SkipIntros, actual.StartArguments.SkipIntros);
                            Assert.AreEqual(expected.StartArguments.DisableFrameLimit, actual.StartArguments.DisableFrameLimit);
                            Assert.AreEqual(expected.StartArguments.EnableCheats, actual.StartArguments.EnableCheats);
                        }
                        break;
                    case AppSettingsGameFs25Data expected:
                        {
                            AppSettingsGameFs25Data actual = data.Games.OfType<AppSettingsGameFs25Data>().Single();
                            Assert.AreEqual(expected.IsEnabled, actual.IsEnabled);
                            Assert.AreEqual(expected.ExeDirectoryPath, actual.ExeDirectoryPath);
                            Assert.AreEqual(expected.DataDirectoryPath, actual.DataDirectoryPath);
                            Assert.AreEqual(expected.StartArguments.SkipIntros, actual.StartArguments.SkipIntros);
                            Assert.AreEqual(expected.StartArguments.DisableFrameLimit, actual.StartArguments.DisableFrameLimit);
                            Assert.AreEqual(expected.StartArguments.EnableCheats, actual.StartArguments.EnableCheats);
                        }
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }

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

            // FS22
            {
                AppSettingsGameFs22 gameSettings = new AppSettingsGameFs22
                {
                    IsEnabled = true,
                    ExeDirectoryPath = @"B:\Games\Giants\Farming Simulator 2022",
                    DataDirectoryPath = @"C:\Users\Oliver Fida\Documents\My Games\FarmingSimulator2022"
                };
                gameSettings.StartArguments.SkipIntros = true;
                gameSettings.StartArguments.DisableFrameLimit = true;
                gameSettings.StartArguments.EnableCheats = false;

                appSettings.Games.Add(gameSettings);
            }

            // FS25
            {
                AppSettingsGameFs25 gameSettings = new AppSettingsGameFs25
                {
                    IsEnabled = false,
                    ExeDirectoryPath = @"B:\Games\Giants\Farming Simulator 2025",
                    DataDirectoryPath = @"C:\Users\Oliver Fida\Documents\My Games\FarmingSimulator2025"
                };
                gameSettings.StartArguments.SkipIntros = true;
                gameSettings.StartArguments.DisableFrameLimit = false;
                gameSettings.StartArguments.EnableCheats = true;

                appSettings.Games.Add(gameSettings);
            }

            return appSettings;
        }

        private AppSettingsData GetAppSettingsData()
        {
            AppSettingsData appSettings = new AppSettingsData
            {
                ApplicationMode = ApplicationMode.Creator
            };

            List<AppSettingsGameDataBase> games = new List<AppSettingsGameDataBase>();

            // FS22
            {
                AppSettingsGameFs22Data gameSettings = new AppSettingsGameFs22Data
                {
                    IsEnabled = true,
                    ExeDirectoryPath = @"B:\Games\Giants\Farming Simulator 2022",
                    DataDirectoryPath = @"C:\Users\Oliver Fida\Documents\My Games\FarmingSimulator2022"
                };
                gameSettings.StartArguments.SkipIntros = true;
                gameSettings.StartArguments.DisableFrameLimit = true;
                gameSettings.StartArguments.EnableCheats = false;

                games.Add(gameSettings);
            }

            // FS25
            {
                AppSettingsGameFs25Data gameSettings = new AppSettingsGameFs25Data
                {
                    IsEnabled = false,
                    ExeDirectoryPath = @"B:\Games\Giants\Farming Simulator 2025",
                    DataDirectoryPath = @"C:\Users\Oliver Fida\Documents\My Games\FarmingSimulator2025"
                };
                gameSettings.StartArguments.SkipIntros = true;
                gameSettings.StartArguments.DisableFrameLimit = false;
                gameSettings.StartArguments.EnableCheats = true;

                games.Add(gameSettings);
            }

            appSettings.Games = games.ToArray();

            return appSettings;
        }
    }
}
