using OF.Base.Objects;

namespace OliverFida.FSimMan.Exceptions.Config
{
    public class InvalidConfigFileException : OfException
    {
        public InvalidConfigFileException(string fileName) : base($"Invalid config file: \"{fileName}\"") { }
    }
}
