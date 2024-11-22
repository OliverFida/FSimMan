namespace OF.Base.Objects
{
    public interface IInitializeable
    {
        public bool IsInitialized { get; }

        public event EventHandler InitializeComplete;
    }
}
