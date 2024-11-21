using OF.Base.Objects;

namespace OliverFida.FSimMan.Exceptions.Fs
{
    public class InvalidFsModFileException : OfException
    {
        public InvalidFsModFileException(string fileName) : base($"Invalid FarmingSim mod file: \"{fileName}\"") { }
    }
}
