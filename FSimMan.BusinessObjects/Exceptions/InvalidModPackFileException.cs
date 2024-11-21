using OF.Base.Objects;

namespace OliverFida.FSimMan.Exceptions
{
    public class InvalidModPackFileException : OfException
    {
        public InvalidModPackFileException(string fileName) : base($"Invalid modpack file: \"{fileName}\"") { }
    }
}
