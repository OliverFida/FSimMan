using OF.Base.Objects;

namespace OF.Base.ViewModel
{
    public abstract class ViewModelBase : BindingObject, IViewModel
    {
        #region Properties
        private readonly bool _isPersistant;
        public bool IsPersistant => _isPersistant;

        private readonly bool _isAutocloseable;
        public bool IsAutocloseable => _isAutocloseable;
        #endregion

        #region Events
        public event EventHandler? ViewModelClosedEvent;
        protected void InvokeViewModelClosedEvent()
        {
            ViewModelClosedEvent?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        #region Constructor
        public ViewModelBase() : this(false) { }

        public ViewModelBase(bool isPersistant) : this(isPersistant, true) { }

        public ViewModelBase(bool isPersistant, bool isAutocloseable) : this(true, isPersistant, isAutocloseable) { }

        public ViewModelBase(bool doInitialize, bool isPersistant, bool isAutocloseable) : base()
        {
            _isPersistant = isPersistant;
            _isAutocloseable = isAutocloseable;
            if (doInitialize) Task.Run(InitializeAsync);
        }
        #endregion

        #region IInitializeable
        public bool IsInitialized { get; private set; } = false;

        public event EventHandler? InitializeComplete;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task InitializeAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            IsInitialized = true;
            InitializeComplete?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
