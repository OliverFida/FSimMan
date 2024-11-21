using OF.Base.Objects;

namespace OF.Base.Client
{
    public abstract class ClientBase : BusyIndicatorManager, IClient
    {
        public ClientBase() : base() { }
    }
}
