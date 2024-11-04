using OliverFida.Base;

namespace OliverFida.FSimMan.Exceptions.Fs
{
    public class NotYetAvailableException : OFException
    {
        public NotYetAvailableException() : base("Oops... This is not yet available! :)") { }
    }
}
