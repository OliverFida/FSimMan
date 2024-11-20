using OliverFida.Base;
using System.Diagnostics;

namespace OliverFida.FSimMan.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        public string ApplicationText
        {
            get => CurrentApplication.AssemblyVersionText;
        }

        public string PublisherText
        {
            get => "Oliver Fida" + Environment.NewLine + "oliver.fi99@gmail.com";
        }

        public Command OpenGitHubCommand { get; } = new Command(OpenGitHubDelegate);
        private static void OpenGitHubDelegate()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "https://github.com/OliverFida/FSimMan",
                UseShellExecute = true
            });
        }
    }
}
