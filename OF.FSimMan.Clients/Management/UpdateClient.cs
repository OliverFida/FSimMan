using OF.Base.Client;
using OF.Base.Objects;
using OF.FSimMan.Api.GitHub;
using OF.FSimMan.Client.Api;
using OliverFida.FSimMan.Exceptions;
using System.Diagnostics;

namespace OF.FSimMan.Client.Management
{
    public class UpdateClient : ClientBase, ISingleton<UpdateClient>
    {
        private GitHubClient _gitHubClient = new GitHubClient("OliverFida", "FSimMan");
        private GitHubReleaseData? _latestRelease = null;
        private HttpClient _downloadClient = new HttpClient();

        #region Properties
        private bool _isUpdateAvailable = false;
        public bool IsUpdateAvailable
        {
            get => _isUpdateAvailable;
            set => SetProperty(ref _isUpdateAvailable, value);
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

                if (_latestRelease is null) return false;

                GitHubReleaseAssetData[] assets = GetValidReleaseAssets();
                if (assets.Length != 1) return false;

                string downloadedFilePath = await DownloadFileAsync(assets[0].DownloadUrl, assets[0].Name);

                BusyContent = "Starting update...";
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = downloadedFilePath,
                        UseShellExecute = true,
                        Verb = "runas"
                    });
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
            _latestRelease = null;
            if (CurrentApplication.AssemblyVersion is null) return false;

            GitHubReleaseData[]? releases = await _gitHubClient.TryGetReleasesAsync();
            if (releases is null || releases.Length == 0) return false;

            _latestRelease = (from r in releases orderby r.TagName descending select r).First();
            (int major, int minor, int build) versionParts = GetVersionParts(_latestRelease.TagName);

            if (versionParts.major < CurrentApplication.AssemblyVersion.Major) return false;
            if (versionParts.minor < CurrentApplication.AssemblyVersion.Minor) return false;
            if (versionParts.build < CurrentApplication.AssemblyVersion.Build) return false;

            return true;
        }

        private (int major, int minor, int build) GetVersionParts(string versionString)
        {
            versionString = versionString.Substring(1);
            string[] parts = versionString.Split('.');

            return (Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1]), Convert.ToInt32(parts[2]));
        }

        private GitHubReleaseAssetData[] GetValidReleaseAssets()
        {
            return (from a in _latestRelease!.Assets where a.Name.EndsWith(".exe") select a).ToArray();
        }

        private async Task<string> DownloadFileAsync(string url, string fileName)
        {
            byte[] fileBytes = await _downloadClient.GetByteArrayAsync(url);

            string targetPath = Path.Combine(CurrentApplication.TEMP_PATH, fileName);
            await File.WriteAllBytesAsync(targetPath, fileBytes);

            return targetPath;
        }
        #endregion

        #region ISingleton
        private static UpdateClient _instance = new UpdateClient();
        public static UpdateClient Instance { get => _instance; }
        #endregion
    }
}
