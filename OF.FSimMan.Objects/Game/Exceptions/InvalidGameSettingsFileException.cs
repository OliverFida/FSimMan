using CLS.Core;

namespace OF.FSimMan.Game.Exceptions
{
    public class InvalidGameSettingsFileException: ClsException
    {
        public InvalidGameSettingsFileException() : base($"Invalid game settings file!") { }
    }
}
