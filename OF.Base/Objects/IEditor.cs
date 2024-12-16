namespace OF.Base.Objects
{
    public interface IEditor<T> : IBindingObject where T : IEditableObject
    {
        public T ObjectToEdit { get; }
    }
}
