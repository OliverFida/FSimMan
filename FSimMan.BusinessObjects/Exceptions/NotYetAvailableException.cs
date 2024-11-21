using OF.Base.Objects;

namespace OliverFida.FSimMan.Exceptions.Fs
{
    public class NotYetAvailableException : OfException
    {
        public NotYetAvailableException() : base("Oops... This is not yet available! :)") { }
    }
}
