﻿using System.Text.Json.Serialization;

namespace OF.FSimMan.Api.GitHub
{
    public class GitHubReleaseAssetData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("browser_download_url")]
        public string DownloadUrl { get; set; } = string.Empty;
    }
}
