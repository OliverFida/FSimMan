using CLS.Core;

namespace OF.FSimMan.Game.Exceptions
{
    public class ModPackNotExistingException: ClsException
    {
        public ModPackNotExistingException() : base($"Modpack doesn't exist!") { }
    }
}
