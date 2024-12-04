using OF.Base.Tests.TestUtility;
using System.ComponentModel;

namespace OF.Base.Tests.Objects
{
    [TestClass]
    [TestCategory("ci")]
    public class BindingObject
    {
        [TestMethod]
        public void Test()
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
