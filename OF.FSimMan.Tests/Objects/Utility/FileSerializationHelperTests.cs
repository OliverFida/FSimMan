using OF.FSimMan.Management;
using OF.FSimMan.Utility;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Objects.Utility
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class FileSerializationHelperTests : FSimManTestsBase
    {
        private readonly string _testFileName = "temp.txt";

        [TestMethod]
        public void FileSerializationHelper_Self()
        {
            AppSettings s = GetAppSettings();
            AppSettingsData d = new AppSettingsData();
            d.ToData(s);

            FileSerializationHelper.SerializeConfigFile(_testFileName, d);
            AppSettingsData d2 = FileSerializationHelper.DeserializeConfigFile<AppSettingsData>(_testFileName);
            AppSettings s2 = d2.FromData();
            Assert.AreEqual(s.ApplicationMode, s2.ApplicationMode);
            Assert.AreEqual(s.IsApplicationModeCreator, s2.IsApplicationModeCreator);
            Assert.AreEqual(s.Games.Count, s2.Games.Count);
            Assert.AreEqual(s.LastSelectedView, s2.LastSelectedView);
            File.Delete(Path.Combine(CurrentApplication.CONFIG_PATH, _testFileName));

            Stream st = new MemoryStream();
            FileSerializationHelper.Serialize(ref st, d);
            d2 = FileSerializationHelper.Deserialize<AppSettingsData>(ref st);
            s2 = d2.FromData();
            Assert.AreEqual(s.ApplicationMode, s2.ApplicationMode);
            Assert.AreEqual(s.IsApplicationModeCreator, s2.IsApplicationModeCreator);
            Assert.AreEqual(s.Games.Count, s2.Games.Count);
            Assert.AreEqual(s.LastSelectedView, s2.LastSelectedView);

            FileSerializationHelper.SerializeFile(_testFileName, d);
            d2 = FileSerializationHelper.DeserializeFile<AppSettingsData>(_testFileName);
            s2 = d2.FromData();
            Assert.AreEqual(s.ApplicationMode, s2.ApplicationMode);
            Assert.AreEqual(s.IsApplicationModeCreator, s2.IsApplicationModeCreator);
            Assert.AreEqual(s.Games.Count, s2.Games.Count);
            Assert.AreEqual(s.LastSelectedView, s2.LastSelectedView);
            File.Delete(_testFileName);
        }

        private AppSettings? _appSettings = null;
        private AppSettings GetAppSettings()
        {
            if (_appSettings is null)
            {
                _appSettings = new AppSettings()
                {
                    ApplicationMode = ApplicationMode.Creator,
                    LastSelectedView = "SomeView"
                };
            }

            return _appSettings;
        }
    }
}
