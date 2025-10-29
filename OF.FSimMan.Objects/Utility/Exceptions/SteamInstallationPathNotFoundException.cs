using OF.Base.Objects;

namespace OF.FSimMan.Utility.Exceptions
{
    public class SteamInstallationPathNotFoundException : OfException
    {
        public SteamInstallationPathNotFoundException() : base($"Steam installation path could not be found!") { }
    }
}
