using OliverFida.Base;

namespace OliverFida.FSimMan.Exceptions.Config
{
    public class InvalidConfigFileException : OFException
    {
        public InvalidConfigFileException(string fileName) : base($"Invalid config file: \"{fileName}\"") { }
    }
}
