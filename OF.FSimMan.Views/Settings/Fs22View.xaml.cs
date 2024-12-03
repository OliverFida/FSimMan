using OF.FSimMan.Client.Management;
using OF.FSimMan.Management.Games.Fs;
using System.Windows.Controls;

namespace OF.FSimMan.View.Settings
{
    /// <summary>
    /// Interaction logic for ApplicationView.xaml
    /// </summary>
    public partial class Fs22View : UserControl
    {
        public Fs22View()
        {
            InitializeComponent();
        }

        private void DebugSelectExeDirectoryPathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if DEBUG
            SettingsClient.Instance.AppSettings.GetGameSettings<AppSettingsGameFs22>().ExeDirectoryPath = @"B:\Games\Giants\Farming Simulator 2022";
#endif
        }

        private void DebugSelectDataDirectoryPathButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if DEBUG
            SettingsClient.Instance.AppSettings.GetGameSettings<AppSettingsGameFs22>().DataDirectoryPath = @"C:\Users\Oliver Fida\Documents\My Games\FarmingSimulator2022";
#endif
        }
    }
}
