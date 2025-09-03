using OF.Base.Objects;

namespace OF.FSimMan.Game.Exceptions
{
    public class ModAlreadyInPackException : OfException
    {
        public ModAlreadyInPackException() : base($"Mod already in pack!") { }
    }
}
