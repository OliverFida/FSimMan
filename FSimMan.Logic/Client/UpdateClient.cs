﻿using OliverFida.Base;
using OliverFida.FSimMan.GitHub;
using System.Diagnostics;

namespace OliverFida.FSimMan.Client
{
    public class UpdateClient : ClientBase, ISingleton<UpdateClient>
    {
        #region ISingleton
        private static UpdateClient _instance = new UpdateClient();
        public static UpdateClient Instance { get => _instance; }
        #endregion

        private GitHubClient _gitHubClient = new GitHubClient("OliverFida", "FSimMan");
        private GitHubReleaseData? _latestRelease = null;
        private HttpClient _downloadClient = new HttpClient();

        #region Constructor
        private UpdateClient()
        {
        }
        #endregion

        #region Methods PUBLIC
        public async Task<bool> GetUpdateAvailableAsync()
        {
            _latestRelease = null;
            if (CurrentApplication.AssemblyVersion is null) return false;

            GitHubReleaseData[]? releases = await _gitHubClient.TryGetReleasesAsync();
            if (releases is null || releases.Length == 0) return false;

            _latestRelease = (from r in releases orderby r.TagName descending select r).First();
            (int major, int minor, int build) versionParts = GetVersionParts(_latestRelease.TagName);

            if (versionParts.major > CurrentApplication.AssemblyVersion.Major ||
                versionParts.minor > CurrentApplication.AssemblyVersion.Minor ||
                versionParts.build > CurrentApplication.AssemblyVersion.Build) return true;

            return false;
        }

        public async Task<bool> TryExecuteUpdateAsync()
        {
            if (_latestRelease is null) return false;

            GitHubReleaseAssetData[] assets = GetValidReleaseAssets();
            if (assets.Length != 1) return false;

            string downloadedFilePath = await DownloadFileAsync(assets[0].DownloadUrl, assets[0].Name);
            Process.Start(new ProcessStartInfo(downloadedFilePath));

            return true;
        }
        #endregion

        #region Methods PRIVATE
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
    }
}