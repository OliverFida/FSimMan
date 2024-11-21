using OF.Base.Objects;

namespace OliverFida.FSimMan.Exceptions
{
    public class ModPackAlreadyExistsException : OfException
    {
        public ModPackAlreadyExistsException() : base($"Modpack already exists!") { }
    }
}
