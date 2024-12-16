using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Objects
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class CurrentApplicationTests : FSimManTestsBase
    {
        [TestMethod]
        public void CurrentApplication_Self()
        {
            Assert.IsTrue(Path.Exists(CurrentApplication.CONFIG_PATH));
            Assert.IsTrue(Path.Exists(CurrentApplication.MODPACKS_PATH));
            Assert.IsTrue(Path.Exists(CurrentApplication.TEMP_PATH));
        }
    }
}
