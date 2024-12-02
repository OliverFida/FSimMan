using OF.Base.Objects;

namespace OF.Base.Client
{
    public abstract class ClientBase : BusyIndicatorManager, IClient
    {
        public ClientBase() : this(true) { }

        public ClientBase(bool doInitialize) : base()
        {
            if (doInitialize) Task.Run(InitializeAsync);
        }

        public bool IsInitialized { get; private set; } = false;

        public event EventHandler? InitializeComplete;

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task InitializeAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            IsBusy = true;
            IsInitialized = true;
            InitializeComplete?.Invoke(this, EventArgs.Empty);
            ResetBusyIndicator();
        }
    }
}
