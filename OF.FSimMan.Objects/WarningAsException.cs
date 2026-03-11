using CLS.Core;

namespace OF.FSimMan
{
    public abstract class WarningAsException: ClsException
    {
        public WarningAsException(string? message) : base(message)
        {
        }
    }
}
