using OF.Base.Objects;

namespace OF.FSimMan
{
    public abstract class WarningAsException : OfException
    {
        public WarningAsException(string? message) : base(message)
        {
        }
    }
}
