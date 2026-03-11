using CLS.Core;

namespace OF.FSimMan.Management.Exceptions
{
    public class DataPathIncorrectException: ClsException
    {
        public DataPathIncorrectException() : base("Data Path does not seem to be correct!") { }
    }
}
