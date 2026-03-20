using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Client.Api
{
    [ExcludeFromCodeCoverage]
    public class GitHubClient : OF.FSimMan.Updater.Clients.Api.GitHubClient
    {
        #region Constructor
        private GitHubClient() : base("OliverFida", "FSimMan")
        {

        }
        #endregion

        #region ISingleton
        private static readonly GitHubClient _instance = new GitHubClient();
        public static GitHubClient Instance => _instance;
        #endregion
    }
}
