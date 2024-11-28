using OF.Base.Objects;

namespace OF.FSimMan.Game.Exceptions
{
    public class ModPackNotExistingException : OfException
    {
        public ModPackNotExistingException() : base($"Modpack doesn't exist!") { }
    }
}
