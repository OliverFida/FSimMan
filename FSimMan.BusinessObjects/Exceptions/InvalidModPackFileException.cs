using OliverFida.Base;

namespace OliverFida.FSimMan.Exceptions
{
    public class InvalidModPackFileException : OFException
    {
        public InvalidModPackFileException(string fileName) : base($"Invalid modpack file: \"{fileName}\"") { }
    }
}
