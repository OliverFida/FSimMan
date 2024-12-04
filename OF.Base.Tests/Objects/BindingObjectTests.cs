using OF.Base.Tests.TestUtility;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace OF.Base.Tests.Objects
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class BindingObjectTests
    {
        [TestMethod]
        public void BindingObject_Self()
        {
            AnyBindingObject obj = new AnyBindingObject();

            bool hasTriggered = false;
            PropertyChangedEventHandler eventHandler = (object? sender, PropertyChangedEventArgs e) => hasTriggered = true;

            obj.PropertyChanged += eventHandler;

            Assert.IsFalse(hasTriggered);

            obj._testString = "Another message";
            Assert.IsFalse(hasTriggered);

            obj.TestString = "Final message";
            Assert.IsTrue(hasTriggered);
        }
    }
}
