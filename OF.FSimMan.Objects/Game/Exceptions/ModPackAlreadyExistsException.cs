using OF.Base.Objects;

namespace OF.FSimMan.Game.Exceptions
{
    public class ModPackAlreadyExistsException : OfException
    {
        public ModPackAlreadyExistsException() : base($"Modpack already exists!") { }
    }
}
