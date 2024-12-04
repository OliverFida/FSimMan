namespace OF.Base.Objects
{
    public interface ICommand : IBindingObject, System.Windows.Input.ICommand
    {
        public object? Parameter { get; }
    }
}
