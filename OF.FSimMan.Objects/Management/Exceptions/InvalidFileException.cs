using OF.Base.Objects;

namespace OF.FSimMan.Management.Exceptions
{
    public class InvalidFileException : OfException
    {
        public InvalidFileException(string fileName) : base($"Invalid file: \"{fileName}\"") { }
    }
}
