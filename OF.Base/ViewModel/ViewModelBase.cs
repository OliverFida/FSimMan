using OF.Base.Objects;

namespace OF.Base.ViewModel
{
    public abstract class ViewModelBase : BindingObject, IViewModel
    {
        public ViewModelBase() : this(true) { }

        public ViewModelBase(bool doInitialize) : base()
        {
            if (doInitialize) Task.Run(InitializeAsync);
        }

        public bool IsInitialized { get; private set; } = false;

        public event EventHandler? InitializeComplete;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task InitializeAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            IsInitialized = true;
            InitializeComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}
