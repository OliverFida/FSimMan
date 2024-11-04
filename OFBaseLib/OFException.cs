namespace OliverFida.Base
{
    public class OFException : Exception
    {
        public OFException() : base() { }
        public OFException(string? message) : base(message) { }
        public OFException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
