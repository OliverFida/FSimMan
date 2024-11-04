using OliverFida.Base;

namespace OliverFida.FSimMan.Exceptions
{
    public class ModPackAlreadyExistsException : OFException
    {
        public ModPackAlreadyExistsException() : base($"Modpack already exists!") { }
    }
}
