using OF.FSimMan.Client.Management;
using OF.FSimMan.Management.Games.Fs;
using System.Windows.Controls;

namespace OF.FSimMan.View.Management.Settings
{
    /// <summary>
    /// Interaction logic for ApplicationView.xaml
    /// </summary>
    public partial class ApplicationView : UserControl
    {
        public ApplicationView()
        {
            InitializeComponent();
        }

        private void DebugTryAutodetectExePathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if DEBUG
            GameSettingsFs25 settings = SettingsClient.Instance.AppSettings.GetGameSettings<GameSettingsFs25>();
            settings.TryAutodetectExePath();
#endif
        }

        private void DebugTryAutodetectDataPathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if DEBUG
            GameSettingsFs25 settings = SettingsClient.Instance.AppSettings.GetGameSettings<GameSettingsFs25>();
            settings.TryAutodetectDataPath();
#endif
        }
    }
}
