namespace OF.Base.Objects
{
    public class Command : BindingObject, ICommand
    {
        private Action? _action = null;

        private object? _parameter = null;
        public object? Parameter { get => _parameter; }

        #region Constructor
        public Command(Action action)
        {
            _action = action;
        }

        public Command(object target, Action<object> action)
        {
            _action = () => action(target);
        }
        #endregion

        #region ICommand
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (_action != null)
            {
                _parameter = parameter;
                _action();
            }
        }
        #endregion
    }
}
