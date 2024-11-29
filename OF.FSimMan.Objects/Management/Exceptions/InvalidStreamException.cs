using OF.Base.Objects;

namespace OF.FSimMan.Management.Exceptions
{
    public class InvalidStreamException : OfException
    {
        public InvalidStreamException() : base("Invalid stream") { }
    }
}
