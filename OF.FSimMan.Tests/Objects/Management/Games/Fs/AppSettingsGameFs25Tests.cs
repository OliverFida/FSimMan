using OF.FSimMan.Management;
using OF.FSimMan.Management.Games.Fs;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Objects.Management
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class AppSettingsGameFs25Tests : FSimManTestsBase
    {
        [TestMethod]
        public void AppSettingsGameFs25_Self()
        {
            bool eventTriggered = false;
            EventHandler<AppSettingsStoreTriggerEventArgs> eH = (object? sender, AppSettingsStoreTriggerEventArgs e) =>
            {
                eventTriggered = true;
            };

            AppSettingsGameFs25Data d = new AppSettingsGameFs25Data();
            AppSettingsGameFs25 s = d.FromData();
            s.StoreTrigger += eH;
            Assert.IsFalse(s.IsEnabled);
            Assert.AreEqual(string.Empty, s.ExeDirectoryPath);
            Assert.AreEqual(string.Empty, s.DataDirectoryPath);
            Assert.IsFalse(s.IsFullyConfigured);
            Assert.IsFalse(eventTriggered);

            s.IsEnabled = true;
            Assert.IsTrue(eventTriggered);

            string path = @"C:\Temp";
            s.ExeDirectoryPath = path;
            s.DataDirectoryPath = path;
            Assert.IsTrue(s.IsFullyConfigured);
            Assert.AreEqual(path, s.ExeDirectoryPath);
            Assert.AreEqual(path, s.DataDirectoryPath);
        }
    }
}
