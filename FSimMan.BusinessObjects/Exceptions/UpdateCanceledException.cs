using OliverFida.Base;

namespace OliverFida.FSimMan.Exceptions
{
    public class UpdateCanceledException : OFException
    {
        public UpdateCanceledException() : base("The update has been canceled.") { }
    }
}
