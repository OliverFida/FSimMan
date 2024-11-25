using OF.Base.Objects;
using OF.Base.ViewModel;
using System.Diagnostics;

namespace OF.FSimMan.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        #region Properties
        public string VersionText { get => CurrentApplication.AssemblyVersionText; }

        public string PublisherText { get => "Oliver Fida" + Environment.NewLine + "oliver.fi99@gmail.com"; }
        #endregion

        #region Commands
        public Command OpenGitHubCommand { get; } = new Command(OpenGitHubDelegate);
        private static void OpenGitHubDelegate()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/OliverFida/FSimMan",
                UseShellExecute = true
            });
        }
        #endregion
    }
}
