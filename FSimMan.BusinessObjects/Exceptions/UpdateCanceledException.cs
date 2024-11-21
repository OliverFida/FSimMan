using OF.Base.Objects;

namespace OliverFida.FSimMan.Exceptions
{
    public class UpdateCanceledException : OfException
    {
        public UpdateCanceledException() : base("The update has been canceled.") { }
    }
}
