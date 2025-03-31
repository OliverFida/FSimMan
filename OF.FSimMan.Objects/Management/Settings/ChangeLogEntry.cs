using OF.FSimMan.Api.GitHub;

namespace OF.FSimMan.Management.Settings
{
    public class ChangeLogEntry
    {
        public string Title { get; }
        public string? ReleaseDateTime { get; }
        public List<string> Content { get; }

        public ChangeLogEntry(GitHubReleaseData release)
        {
            Title = release.TagName;
            ReleaseDateTime = release.ReleaseDateTime?.ToString("dd.MM.yyyy HH:mm");
            Content = release.Body.Split(Environment.NewLine).ToList();
        }
    }
}
