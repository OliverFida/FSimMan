using OF.Base.Client;
using OF.FSimMan.Api.GitHub;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace OF.FSimMan.Client.Api
{
    [ExcludeFromCodeCoverage]
    public class GitHubClient : ClientBase
    {
        private const string _githubUrl = "https://api.github.com";
        private string _repositoryPath;
        private string _releasesPath;

        private HttpClient _httpClient;

        #region Consructor
        public GitHubClient(string author, string repository) : base()
        {
            _repositoryPath = $"/repos/{author}/{repository}";
            _releasesPath = _repositoryPath + "/releases";

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_githubUrl)
            };
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "CSharp-App");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github+json");
            _httpClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
        }
        #endregion

        #region Methods PUBLIC
        public async Task<GitHubReleaseData[]?> TryGetReleasesAsync()
        {
            string response = await GetResponseStringAsync(_releasesPath);
            if (response.Equals(string.Empty)) return null;

            return (from r in JsonSerializer.Deserialize<GitHubReleaseData[]>(response) where r.IsDraft == false && r.IsPreRelease == false select r).ToArray();
        }
        #endregion

        #region Methods PRIVATE
        private async Task<HttpResponseMessage?> GetResponseAsync(string path)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(path);
                response.EnsureSuccessStatusCode();

                return response;
            }
            catch
            {
                return null;
            }
        }

        private async Task<string> GetResponseStringAsync(string path)
        {
            HttpResponseMessage? response = await GetResponseAsync(path);
            if (response is null || response.Content is null) return string.Empty;

            return await response.Content.ReadAsStringAsync();
        }
        #endregion
    }
}
