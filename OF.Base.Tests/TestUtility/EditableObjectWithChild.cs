namespace OF.Base.Tests.TestUtility
{
    public class EditableObjectWithChild : AnyEditableObject
    {
        private AnyEditableObject _child = new AnyEditableObject();
        public AnyEditableObject Child
        {
            get => _child;
            protected set => _child = value;
        }
    }
}
