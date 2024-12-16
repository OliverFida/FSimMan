using OF.Base.Tests.TestUtility;
using System.Diagnostics.CodeAnalysis;

namespace OF.Base.Tests.Objects
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class EditorBaseTests
    {
        [TestMethod]
        public void EditorBase_Self()
        {
            AnyEditableObject orgObj = new AnyEditableObject();
            AnyEditor editor = new AnyEditor();
            Assert.AreEqual(orgObj.TestString, editor.ObjectToEdit.TestString);

            string message = "First message";
            editor.TriggerBeginEdit();
            editor.ObjectToEdit.TestString = message;
            editor.TriggerCancelEdit();
            Assert.AreEqual(orgObj.TestString, editor.ObjectToEdit.TestString);

            editor.TriggerBeginEdit();
            editor.ObjectToEdit.TestString = message;
            editor.TriggerEndEdit();
            Assert.AreEqual(message, editor.ObjectToEdit.TestString);
        }
    }
}
