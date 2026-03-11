using CLS.Core;

namespace OF.FSimMan.Management.Exceptions
{
    public class InvalidStreamException: ClsException
    {
        public InvalidStreamException() : base("Invalid stream") { }
    }
}
