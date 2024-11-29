using OF.Base.Objects;

namespace OF.FSimMan.ImportExport.Fsmmp.Exceptions
{
    public class InvalidModPackFileException : OfException
    {
        public InvalidModPackFileException(string fileName) : base($"Invalid modpack file: \"{fileName}\"") { }
    }
}
