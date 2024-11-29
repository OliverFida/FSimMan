using OF.Base.Objects;

namespace OF.FSimMan.Game.Exceptions
{
    public class InvalidGameSettingsFileException : OfException
    {
        public InvalidGameSettingsFileException() : base($"Invalid game settings file!") { }
    }
}
