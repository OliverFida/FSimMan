using OF.Base.Objects;

namespace OliverFida.FSimMan.Exceptions
{
    public class MissingModFileException : OfException
    {
        public MissingModFileException(string fileName) : base($"Missing mod file: \"{fileName}\"") { }
    }
}
