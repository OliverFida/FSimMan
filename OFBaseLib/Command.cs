﻿using System.Windows.Input;

namespace OliverFida.Base
{
    public class Command : BindingObjectBase, ICommand
    {
        private Action? _action = null;
        private object? _target;

        private object? _parameter = null;
        public object? Parameter { get => _parameter; }

        private bool _enabled = true;
        public bool Enabled
        {
            get => _enabled;
        }

        #region Constructor
        public Command(Action action)
        {
            _action = action;
        }

        public Command(object target, Action<object> action)
        {
            _target = target;
            _action = () => action(target);
        }
        #endregion

        #region ICommand
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return Enabled;
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
