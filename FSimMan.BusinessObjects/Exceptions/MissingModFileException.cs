using OliverFida.Base;

namespace OliverFida.FSimMan.Exceptions
{
    public class MissingModFileException : OFException
    {
        public MissingModFileException(string fileName) : base($"Missing mod file: \"{fileName}\"") { }
    }
}
