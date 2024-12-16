using OF.FSimMan.Management.Games.Fs;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Objects.Management
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class AppSettingsGameFs22Tests
    {
        [TestMethod]
        public void AppSettingsGameFs22_Self()
        {
            bool eventTriggered = false;
            EventHandler eH = (object? sender, EventArgs e) =>
            {
                eventTriggered = true;
            };

            AppSettingsGameFs22Data d = new AppSettingsGameFs22Data();
            AppSettingsGameFs22 s = d.FromData();
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
