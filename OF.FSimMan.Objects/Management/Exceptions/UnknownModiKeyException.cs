using OF.Base.Objects;

namespace OF.FSimMan.Management.Exceptions
{
    public class UnknownModiKeyException : OfException
    {
        public UnknownModiKeyException() : base("Unknown modification key!") { }
    }
}
