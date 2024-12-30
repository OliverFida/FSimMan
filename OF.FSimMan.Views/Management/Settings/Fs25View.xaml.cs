using OF.FSimMan.Client.Management;
using OF.FSimMan.Management.Games.Fs;
using System.Windows.Controls;

namespace OF.FSimMan.View.Management.Settings
{
    /// <summary>
    /// Interaction logic for ApplicationView.xaml
    /// </summary>
    public partial class Fs25View : UserControl
    {
        public Fs25View()
        {
            InitializeComponent();
        }

        private void DebugSelectExeDirectoryPathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if DEBUG
            string path = @"B:\Games\Giants\Farming Simulator 2025";
            AppSettingsGameFs25 settings = SettingsClient.Instance.AppSettings.GetGameSettings<AppSettingsGameFs25>();
            settings.ValidateExeDirectoryPath(path);
            settings.ExeDirectoryPath = path;
#endif
        }

        private void DebugClearExeDirectoryPathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if DEBUG
            AppSettingsGameFs25 settings = SettingsClient.Instance.AppSettings.GetGameSettings<AppSettingsGameFs25>();
            settings.ExeDirectoryPath = string.Empty;
#endif
        }

        private void DebugSelectDataDirectoryPathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if DEBUG
            string path = @"C:\Users\Oliver Fida\Documents\My Games\FarmingSimulator2025";
            AppSettingsGameFs25 settings = SettingsClient.Instance.AppSettings.GetGameSettings<AppSettingsGameFs25>();
            settings.ValidateDataDirectoryPath(path);
            settings.DataDirectoryPath = path;
#endif
        }

        private void DebugClearDataDirectoryPathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if DEBUG
            AppSettingsGameFs25 settings = SettingsClient.Instance.AppSettings.GetGameSettings<AppSettingsGameFs25>();
            settings.DataDirectoryPath = string.Empty;
#endif
        }
    }
}
