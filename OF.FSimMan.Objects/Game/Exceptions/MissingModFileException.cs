using CLS.Core;

namespace OF.FSimMan.Game.Exceptions
{
    public class MissingModFileException: ClsException
    {
        public MissingModFileException(string fileName) : base($"Missing mod file: \"{fileName}\"") { }
    }
}
