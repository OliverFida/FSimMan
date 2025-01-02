using OF.FSimMan.Management;
using OF.FSimMan.Management.Games.Fs;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Objects.Management
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class AppSettingsTests : FSimManTestsBase
    {
        [TestMethod]
        public void AppSettings_Self()
        {
            AppSettingsData d = new AppSettingsData();
            AppSettings s = d.FromData();
            Assert.AreEqual(ApplicationMode.User, s.ApplicationMode);
            Assert.AreEqual(false, s.IsApplicationModeCreator);
            Assert.AreEqual(0, s.Games.Count);

            s = new AppSettings();
            Assert.AreEqual(ApplicationMode.User, s.ApplicationMode);
            Assert.AreEqual(false, s.IsApplicationModeCreator);
            Assert.AreEqual(0, s.Games.Count);

            bool eventTriggered = false;
            EventHandler<AppSettingsStoreTriggerEventArgs> eH = (object? sender, AppSettingsStoreTriggerEventArgs e) =>
            {
                eventTriggered = true;
            };
            s.StoreTrigger += eH;
            s.ApplicationMode = ApplicationMode.Creator;
            s.Games.Add(new GameSettingsFs22());
            Assert.AreEqual(ApplicationMode.Creator, s.ApplicationMode);
            Assert.AreEqual(true, s.IsApplicationModeCreator);
            Assert.AreEqual(1, s.Games.Count);
            Assert.IsTrue(eventTriggered);
        }
    }
}
