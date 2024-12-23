using OF.Base.Objects;

namespace OF.FSimMan.Management.Exceptions
{
    public class GamePathIncorrectException : OfException
    {
        public GamePathIncorrectException() : base("Game Path does not seem to be correct!") { }
    }
}
