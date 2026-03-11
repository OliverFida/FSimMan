using CLS.Core;

namespace OF.FSimMan.Utility.Exceptions
{
    public class SteamInstallationPathNotFoundException: ClsException
    {
        public SteamInstallationPathNotFoundException() : base($"Steam installation path could not be found!") { }
    }
}
