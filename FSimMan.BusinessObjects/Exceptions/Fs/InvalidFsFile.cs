using OF.Base.Objects;

namespace OliverFida.FSimMan.Exceptions.Fs
{
    public class InvalidFsFileException : OfException
    {
        public InvalidFsFileException(string fileName) : base($"Invalid FarmingSim file: \"{fileName}\"") { }
    }
}
