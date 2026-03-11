using CLS.Core;

namespace OF.FSimMan.Management.Exceptions
{
    public class InvalidFileException: ClsException
    {
        public InvalidFileException(string fileName) : base($"Invalid file: \"{fileName}\"") { }
    }
}
