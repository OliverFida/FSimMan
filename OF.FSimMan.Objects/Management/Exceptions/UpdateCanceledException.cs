using CLS.Core;

namespace OliverFida.FSimMan.Exceptions
{
    public class UpdateCanceledException: ClsException
    {
        public UpdateCanceledException() : base("The update has been canceled.") { }
    }
}
