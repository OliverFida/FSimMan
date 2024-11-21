namespace OF.Base.Objects
{
    public interface IEditableObject : System.ComponentModel.IEditableObject
    {
        public bool IsEditing { get; }
        public bool IsModified { get; }
    }
}
