using CLS.Core;

namespace OF.FSimMan.Game.Exceptions
{
    public class ModPackAlreadyExistsException: ClsException
    {
        public ModPackAlreadyExistsException() : base($"Modpack already exists!") { }
    }
}
