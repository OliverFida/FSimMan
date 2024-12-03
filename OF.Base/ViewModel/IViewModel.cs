using OF.Base.Objects;

namespace OF.Base.ViewModel
{
    public interface IViewModel : IBindingObject, IInitializeable
    {
        public bool IsDebug { get; }
        public bool IsPersistant { get; }
        public bool IsAutocloseable { get; }
        public bool PreventAutoclose { get; }

        public event EventHandler? ViewModelClosedEvent;

        public void ExecutePreventAutoclose(Action action);
    }
}
