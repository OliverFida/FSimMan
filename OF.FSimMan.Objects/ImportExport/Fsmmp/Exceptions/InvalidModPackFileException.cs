using CLS.Core;

namespace OF.FSimMan.ImportExport.Fsmmp.Exceptions
{
    public class InvalidModPackFileException: ClsException
    {
        public InvalidModPackFileException(string fileName) : base($"Invalid modpack file: \"{fileName}\"") { }
    }
}
