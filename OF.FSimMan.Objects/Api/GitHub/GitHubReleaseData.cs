﻿using System.Text.Json.Serialization;

namespace OF.FSimMan.Api.GitHub
{
    public class GitHubReleaseData
    {
        [JsonPropertyName("tag_name")]
        public string TagName { get; set; } = string.Empty;

        [JsonPropertyName("draft")]
        public bool IsDraft { get; set; } = true;

        [JsonPropertyName("prerelease")]
        public bool IsPreRelease { get; set; } = true;

        [JsonPropertyName("published_at")]
        public string PublishedAt { get; set; } = string.Empty;
        public DateTime? ReleaseDateTime { get => !string.IsNullOrEmpty(PublishedAt) ? DateTime.Parse(PublishedAt) : null; }

        [JsonPropertyName("body")]
        public string Body { get; set; } = string.Empty;

        [JsonPropertyName("assets")]
        public GitHubReleaseAssetData[] Assets { get; set; } = [];
    }
}
