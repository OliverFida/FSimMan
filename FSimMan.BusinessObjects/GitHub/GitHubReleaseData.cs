using System.Text.Json.Serialization;

namespace OliverFida.FSimMan.GitHub
{
    public class GitHubReleaseData
    {
        [JsonPropertyName("tag_name")]
        public string TagName { get; set; } = string.Empty;

        [JsonPropertyName("draft")]
        public bool IsDraft { get; set; } = true;

        [JsonPropertyName("prerelease")]
        public bool IsPreRelease { get; set; } = true;

        [JsonPropertyName("assets")]
        public GitHubReleaseAssetData[] Assets { get; set; } = [];
    }
}
