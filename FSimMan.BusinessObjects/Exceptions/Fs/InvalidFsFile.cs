using OliverFida.Base;

namespace OliverFida.FSimMan.Exceptions.Fs
{
    public class InvalidFsFileException : OFException
    {
        public InvalidFsFileException(string fileName) : base($"Invalid FarmingSim file: \"{fileName}\"") { }
    }
}
