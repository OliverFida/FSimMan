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
            SettingsClient.Instance.AppSettings.GetGameSettings<AppSettingsGameFs25>().ExeDirectoryPath = @"B:\Games\Giants\Farming Simulator 2025";
#endif
        }

        private void DebugSelectDataDirectoryPathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if DEBUG
            SettingsClient.Instance.AppSettings.GetGameSettings<AppSettingsGameFs25>().DataDirectoryPath = @"C:\Users\Oliver Fida\Documents\My Games\FarmingSimulator2025";
#endif
        }
    }
}
