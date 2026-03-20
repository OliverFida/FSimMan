using CLS.Core;

namespace OF.FSimMan.Updater.Exceptions
{
    public class UpdateCanceledException : ClsException
    {
        public UpdateCanceledException() : base("The update has been canceled.") { }
    }
}
