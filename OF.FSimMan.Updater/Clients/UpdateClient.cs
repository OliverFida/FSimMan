using CLS.Core;
using CLS.Core.Client;
using OF.FSimMan.Api.GitHub;
using OF.FSimMan.Client.Api;
using OliverFida.FSimMan.Exceptions;
using System.Diagnostics;
using System.IO;
using System.Net.Http;

namespace OF.FSimMan.Updater.Clients
{
    public class UpdateClient : ClientBase, ISingleton<UpdateClient>
    {
        private GitHubClient _gitHubClient = GitHubClient.Instance;
        private HttpClient _downloadClient = new HttpClient();
        private readonly string[] _installerArguments = [
            "verysilent",
            "norestart",
            "mergetasks=\"desktopicon\""
        ];

        #region Properties
        private bool _isUpdateAvailable = false;
        public bool IsUpdateAvailable
        {
            get => _isUpdateAvailable;
            set => SetProperty(ref _isUpdateAvailable, value);
        }

        private GitHubReleaseData? _latestRelease = null;
        public GitHubReleaseData? LatestRelease
        {
            get => _latestRelease;
            private set => SetProperty(ref _latestRelease, value);
        }
        #endregion

        #region Constructor
        private UpdateClient() : base() { }
        #endregion

        #region Methods PUBLIC
        public async Task CheckUpdateAvailableAsync()
        {
            try
            {
                IsBusy = true;
                BusyContent = "Checking for updates...";

                IsUpdateAvailable = await GetUpdateAvailableAsync();
            }
            finally
            {
                ResetBusyIndicator();
            }
        }

        public async Task<bool> TryExecuteUpdateAsync()
        {
            try
            {
                IsBusy = true;
                BusyContent = "Downloading update...";

                if (LatestRelease is null) return false;

                GitHubReleaseAssetData[] assets = GetValidReleaseAssets();
                if (assets.Length != 1) return false;

                string downloadedFilePath = await DownloadFileAsync(assets[0].DownloadUrl, assets[0].Name);

                BusyContent = "Starting update...";
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo
                    {
                        FileName = downloadedFilePath,
                        Arguments = GetInstallerArgs(),
                        UseShellExecute = false
                    };

                    Process p = new Process
                    {
                        StartInfo = psi,
                    };

                    p.Start();
                    await p.WaitForExitAsync();

                    // OFDOI: Handle not successful
                }
                catch
                {
                    throw new UpdateCanceledException();
                }

                return true;
            }
            finally
            {
                ResetBusyIndicator();
            }
        }
        #endregion

        #region Methods PRIVATE
        private async Task<bool> GetUpdateAvailableAsync()
        {
            LatestRelease = null;
            if (CurrentApplication.AssemblyVersion is null) return false;

            GitHubReleaseData[]? releases = await _gitHubClient.TryGetReleasesAsync();
            if (releases is null || releases.Length == 0) return false;

            LatestRelease = (from r in releases orderby r.TagName descending select r).First();
            (int major, int minor, int build) versionParts = GetVersionParts(LatestRelease.TagName);

            if (CurrentApplication.AssemblyVersion.Major < versionParts.major) return true; // 0.x.x -> 1.x.x
            if (CurrentApplication.AssemblyVersion.Major == versionParts.major) // 0.x.x -> 0.y.x
            {
                if (CurrentApplication.AssemblyVersion.Minor < versionParts.minor) return true; // 0.0.x -> 0.1.x
                if (CurrentApplication.AssemblyVersion.Minor == versionParts.minor) // 0.0.x -> 0.0.y
                {
                    if (CurrentApplication.AssemblyVersion.Build < versionParts.build) return true; // 0.0.0 -> 0.0.1
                }
            }

            return false; // No update
        }

        private (int major, int minor, int build) GetVersionParts(string versionString)
        {
            versionString = versionString.Substring(1);
            string[] parts = versionString.Split('.');

            return (Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
        }

        private GitHubReleaseAssetData[] GetValidReleaseAssets()
        {
            return (from a in LatestRelease!.Assets where a.Name.EndsWith(".exe") select a).ToArray();
        }

        private async Task<string> DownloadFileAsync(string url, string fileName)
        {
            byte[] fileBytes = await _downloadClient.GetByteArrayAsync(url);

            string targetPath = Path.Combine(CurrentApplication.TEMP_PATH, fileName);
            await File.WriteAllBytesAsync(targetPath, fileBytes);

            return targetPath;
        }

        private string GetInstallerArgs()
        {
            string retVal = string.Empty;

            foreach (string arg in _installerArguments)
            {
                retVal += $"/{arg} ";
            }

            return retVal;
        }
        #endregion

        #region ISingleton
        private static UpdateClient _instance = new UpdateClient();
        public static UpdateClient Instance { get => _instance; }
        #endregion
    }
}
