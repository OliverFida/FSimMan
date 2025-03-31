using OF.Base.Client;
using OF.Base.Objects;
using OF.FSimMan.Api.GitHub;
using OF.FSimMan.Client.Api;
using OF.FSimMan.Management.Settings;

namespace OF.FSimMan.Client.Management.Settings
{
    public class ChangeLogClient : ClientBase, ISingleton<ChangeLogClient>
    {
        #region Properties
        private GitHubClient _gitHubClient = GitHubClient.Instance;

        private readonly List<ChangeLogEntry> _entries = new List<ChangeLogEntry>();
        public List<ChangeLogEntry> Entries => _entries;
        #endregion

        #region Constructor & Initialize
        private ChangeLogClient() { }

        public override async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                await RefreshChangeLogAsync();
            }
            finally
            {
                await base.InitializeAsync();
            }
        }
        #endregion

        #region Methods PRIVATE
        private async Task RefreshChangeLogAsync()
        {
            _entries.Clear();

            List<GitHubReleaseData>? releases = (await _gitHubClient.TryGetReleasesAsync())?.ToList();
            if (releases is not null)
            {
                List<ChangeLogEntry> temp = new List<ChangeLogEntry>();
                foreach (GitHubReleaseData release in releases)
                {
                    ChangeLogEntry entry = new ChangeLogEntry(release);
                    if (entry.Content.Count > 0 && (entry.Content.Count > 1 || !entry.Content.First().Equals(string.Empty))) temp.Add(entry);
                }
                _entries.AddRange(temp.OrderByDescending(x => x.Title));
            }

            OnPropertyChanged(nameof(Entries));
        }
        #endregion

        #region ISingleton
        private static readonly ChangeLogClient _instance = new ChangeLogClient();
        public static ChangeLogClient Instance => _instance;
        #endregion
    }
}
