using OF.Base.Objects;

namespace OF.FSimMan.Management.Exceptions
{
    public class DataPathIncorrectException : OfException
    {
        public DataPathIncorrectException() : base("Data Path does not seem to be correct!") { }
    }
}
