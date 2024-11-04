using OliverFida.Base;

namespace OliverFida.FSimMan.Exceptions.Fs
{
    public class InvalidFsModFileException : OFException
    {
        public InvalidFsModFileException(string fileName) : base($"Invalid FarmingSim mod file: \"{fileName}\"") { }
    }
}
