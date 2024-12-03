using OF.Base.Objects;

namespace OF.Base.Tests.TestUtility
{
    public class AnyBindingObject : BindingObject
    {
        internal string _testString = "Test message for TestString";
        public string TestString
        {
            get => _testString;
            set => SetProperty(ref _testString, value);
        }
    }
}
