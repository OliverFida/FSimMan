using CLS.Core;

namespace OF.FSimMan.Game.Exceptions
{
    public class InvalidModFileException: ClsException
    {
        public InvalidModFileException(string fileName) : base($"Invalid mod file: \"{fileName}\"") { }
    }
}
