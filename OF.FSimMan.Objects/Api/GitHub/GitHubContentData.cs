using System.Text.Json.Serialization;

namespace OF.FSimMan.Api.GitHub
{
    public class GitHubContentData
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("download_url")]
        public string DownloadUrl { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }
}
