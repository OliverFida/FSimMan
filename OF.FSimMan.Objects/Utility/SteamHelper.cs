using Microsoft.Win32;
using OF.FSimMan.Utility.Exceptions;
using System.Collections.ObjectModel;
using System.Diagnostics;

#pragma warning disable CA1416 // Validate platform compatibility
namespace OF.FSimMan.Utility
{
    public static class SteamHelper
    {
        #region Constants
        private static ReadOnlyCollection<string> POSSIBLE_REGISTRY_KEYS = new ReadOnlyCollection<string>([
            @"HKEY_CURRENT_USER\Software\Valve\Steam",
            @"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Valve\Steam",
            @"HKEY_LOCAL_MACHINE\SOFTWARE\Valve\Steam"
        ]);
        #endregion

        #region Properties
        private static string? _installationPath = null;
        private static string? _exePath = null;
        #endregion

        #region Methods PUBLIC
        public static void LaunchGame(string appId, string startArgs)
        {
            if (_exePath is null) GetExePath();

            string arguments = $"-applaunch {appId}";

            if (!String.IsNullOrWhiteSpace(startArgs)) arguments = $"{arguments} {startArgs}";

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = _exePath,
                Arguments = arguments,
                UseShellExecute = true,
            };
            Process.Start(psi);
        }
        #endregion

        #region Methods PRIVATE
        private static void GetInstallationPath()
        {
            if (_installationPath is not null) return;

            foreach (string key in POSSIBLE_REGISTRY_KEYS)
            {
                string? temp = Registry.GetValue(key, "InstallPath", null) as string;
                if (temp is not null)
                {
                    _installationPath = temp;
                    return;
                }
            }

            throw new SteamInstallationPathNotFoundException();
        }

        private static void GetExePath()
        {
            if (_exePath is not null) return;

            if (_installationPath is null) GetInstallationPath();

            _exePath = Path.Combine(_installationPath!, "steam.exe");
        }
        #endregion
    }
}
#pragma warning restore CA1416 // Validate platform compatibility