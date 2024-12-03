using OF.Base.Objects;

namespace OF.Base.Tests.TestUtility
{
    public class AnyEditableObject : EditableObject
    {
        internal string _testString = "Test message for TestString";
        public string TestString
        {
            get => _testString;
            set => SetProperty(ref _testString, value);
        }
    }
}
