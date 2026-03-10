using OF.Base.Client;
using OF.FSimMan.Api.GitHub;
using OF.FSimMan.Client.Api;
using System.Collections.ObjectModel;

namespace OF.FSimMan.Client
{
    public class HomeClient : ClientBase
    {
        #region Properties
        private GitHubClient _gitHubClient = GitHubClient.Instance;

        private readonly List<NewsEntry> _entries = new List<NewsEntry>();
        public ObservableCollection<NewsEntry> Entries
        {
            get => new ObservableCollection<NewsEntry>(_entries);
        }
        #endregion

        #region Initialize
        public override async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                await RefreshNewsAsync();
            }
            finally
            {
                await base.InitializeAsync();
            }
        }
        #endregion

        #region Methods PRIVATE
        private async Task RefreshNewsAsync()
        {
            _entries.Clear();

            List<GitHubContentData>? newsPosts = (await _gitHubClient.TryGetNewsAsync())?.ToList();
            if (newsPosts is not null)
            {
                List<NewsEntry> temp = new List<NewsEntry>();
                foreach (GitHubContentData newsPost in newsPosts)
                {
                    List<string>? content = (await _gitHubClient.TryDownloadContent(newsPost))?.ToList();
                    if (content is not null) temp.Add(new NewsEntry(newsPost, content));
                }

                _entries.AddRange(temp.OrderByDescending(e => e.DateTime));
            }

            OnPropertyChanged(nameof(Entries));
        }
        #endregion
    }
}
