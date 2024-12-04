using OF.Base.Tests.TestUtility;
using System.Diagnostics.CodeAnalysis;

namespace OF.Base.Tests.Objects
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class BusyIndicatorManagerTests
    {
        private const string _defaultBusyContent = "Please wait...";

        [TestMethod]
        public void BusyIndicatorManager_Self()
        {
            string newContent = "Hoidaus!";
            AnyBusyIndicatorManager bim = new AnyBusyIndicatorManager();
            Assert.IsFalse(bim.IsBusy);
            Assert.AreEqual(_defaultBusyContent, bim.BusyContent);

            try
            {
                bim.IsBusy = true;
                bim.BusyContent = newContent;
                Assert.IsTrue(bim.IsBusy);
                Assert.AreEqual(newContent, bim.BusyContent);
            }
            finally
            {
                bim.ResetBusyIndicator();
            }
            Assert.IsFalse(bim.IsBusy);
            Assert.AreEqual(_defaultBusyContent, bim.BusyContent);
        }
    }
}
