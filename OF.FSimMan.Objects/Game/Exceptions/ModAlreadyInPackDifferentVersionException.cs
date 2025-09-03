using OF.Base.Objects;

namespace OF.FSimMan.Game.Exceptions
{
    public class ModAlreadyInPackDifferentVersionException : OfException
    {
        public ModAlreadyInPackDifferentVersionException() : base($"Mod with different version already in pack!") { }
    }
}
