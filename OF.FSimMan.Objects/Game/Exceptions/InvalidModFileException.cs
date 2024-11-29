using OF.Base.Objects;

namespace OF.FSimMan.Game.Exceptions
{
    public class InvalidModFileException : OfException
    {
        public InvalidModFileException(string fileName) : base($"Invalid mod file: \"{fileName}\"") { }
    }
}
