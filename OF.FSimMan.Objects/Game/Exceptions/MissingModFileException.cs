using OF.Base.Objects;

namespace OF.FSimMan.Game.Exceptions
{
    public class MissingModFileException : OfException
    {
        public MissingModFileException(string fileName) : base($"Missing mod file: \"{fileName}\"") { }
    }
}
