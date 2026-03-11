using CLS.Core;

namespace OF.FSimMan.Game.Exceptions
{
    public class ModAlreadyInPackDifferentVersionException: ClsException
    {
        public ModAlreadyInPackDifferentVersionException() : base($"Mod with different version already in pack!") { }
    }
}
