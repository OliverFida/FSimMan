using OF.Base.Objects;
using OF.Base.Tests.TestUtility;
using System.Diagnostics.CodeAnalysis;

namespace OF.Base.Tests.Objects
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CommandTests
    {
        private Command? _command;

        [TestMethod]
        [TestCategory("ci")]
        public void Command_AllTests()
        {
            Command_Void();
            Command_Parameter();
        }

        [TestMethod]
        public void Command_Void()
        {
            bool buttonPressed = false;

            Action cmdAct = () => buttonPressed = true;
            _command = new Command(cmdAct);
            Assert.IsTrue(_command.CanExecute(null));

            _command.Execute(null);
            Assert.IsTrue(buttonPressed);
        }

        [TestMethod]
        public void Command_Parameter()
        {
            AnyBindingObject? passedParameter = null;

            Action cmdAct = () => passedParameter = (AnyBindingObject?)_command!.Parameter;
            _command = new Command(cmdAct);
            AnyBindingObject parameter = new AnyBindingObject();
            Assert.IsTrue(_command.CanExecute(null));
            Assert.AreEqual(null, passedParameter);

            _command.Execute(parameter);
            Assert.AreEqual(parameter, passedParameter);

            _command.Execute(null);
            Assert.AreEqual(null, passedParameter);
        }
    }
}
