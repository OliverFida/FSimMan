using CLS.Core;

namespace OF.FSimMan.Game.Exceptions
{
    public class ModAlreadyInPackException: ClsException
    {
        public ModAlreadyInPackException() : base($"Mod already in pack!") { }
    }
}
