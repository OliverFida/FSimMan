using OF.Base.Tests.TestUtility;
using System.Diagnostics.CodeAnalysis;

namespace OF.Base.Tests.Objects
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditableObjectTests
    {
        [TestMethod]
        [TestCategory("ci")]
        public void EditableObject_AllTests()
        {
            EditableObject_OnlySelf();
            EditableObject_WithObject();
            EditableObject_WithObservableCollection();
        }

        public void EditableObject_OnlySelf()
        {
            AnyEditableObject obj = new AnyEditableObject();
            Assert.AreEqual("Test message for TestString", obj.TestString);

            string message1 = "First message";
            obj.TestString = message1;
            Assert.AreEqual(message1, obj.TestString);

            string message2 = "Second message";
            obj.BeginEdit();
            obj.TestString = message2;
            Assert.AreEqual(message2, obj.TestString);

            obj.CancelEdit();
            Assert.AreEqual(message1, obj.TestString);

            obj.BeginEdit();
            obj.TestString = message2;
            obj.EndEdit();
            Assert.AreEqual(message2, obj.TestString);
        }

        [TestMethod]
        public void EditableObject_WithObject()
        {
            EditableObjectWithChild obj = new EditableObjectWithChild();

            string message = "Childs message";
            obj.BeginEdit();
            obj.Child.TestString = message;
            obj.CancelEdit();
            Assert.AreEqual(obj.TestString, obj.Child.TestString);

            obj.BeginEdit();
            obj.Child.TestString = message;
            obj.EndEdit();
            Assert.AreEqual(message, obj.Child.TestString);
        }

        [TestMethod]
        public void EditableObject_WithObservableCollection()
        {
            // OFDO
        }
    }
}
