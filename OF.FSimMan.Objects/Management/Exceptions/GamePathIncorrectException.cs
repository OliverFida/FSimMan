using CLS.Core;

namespace OF.FSimMan.Management.Exceptions
{
    public class GamePathIncorrectException: ClsException
    {
        public GamePathIncorrectException() : base("Game Path does not seem to be correct!") { }
    }
}
