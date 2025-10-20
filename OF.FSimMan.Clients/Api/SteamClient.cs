using Microsoft.Win32;
using OF.Base.Client;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.Background;

namespace OF.FSimMan.Client.Api
{
    public class SteamClient : ClientBase
    {
        #region Methods PUBLIC
        public string GetInstallationPath()
        {
            string? path = null;

            // 64-bit registry
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Valve\Steam"))
            {
                path = key?.GetValue("InstallPath") as string;
            }
            if (path is not null) return path;

            // 32-bit registry (SHOULD NEVER BE TRIGGERED, SINCE FSimMan IS ONLY DEPLOYED AS x64 APP)
            using (var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Valve\Steam"))
            {
                path = key?.GetValue("InstallPath") as string;
            }
            if (path is not null) return path;

            throw new NotImplementedException();
        }

        public List<SteamLibraryFolder> GetLibraryPaths()
        {
            string installPath = GetInstallationPath();
            string libraryFilePath = Path.Combine(installPath, "steamapps", "libraryfolders.vdf");
            if (!File.Exists(libraryFilePath))
                throw new NotImplementedException();

            string fileContent = File.ReadAllText(libraryFilePath);

            List<SteamLibraryFolder> libraries = new List<SteamLibraryFolder>();

            // OFODI: GetLibraryPaths()

            return libraries;
        }
        #endregion

        #region ISingleton
        private static readonly SteamClient _instance = new SteamClient();
        public static SteamClient Instance => _instance;

        private SteamClient() { }
        #endregion

        public class SteamLibraryFolder // OFDOI: Extract
        {
            private string _path;
            public string Path
            {
                get => _path;
            }

            private List<string> _appIds;
            public List<string> AppIds
            {
                get => _appIds;
            }

            public SteamLibraryFolder(string path)
            {
                _path = path;
                _appIds = new List<string>();
            }
        }
    }
}
