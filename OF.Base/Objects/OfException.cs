namespace OF.Base.Objects
{
    public class OfException : Exception
    {
        public OfException() : base() { }
        public OfException(string? message) : base(message) { }
        public OfException(string? message, Exception? innerException) : base(message, innerException) { }
    }
}
