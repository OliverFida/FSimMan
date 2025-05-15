using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;
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

        private void DebugModiKeyButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
#if DEBUG
            string key = "2025!E83.Sync";
            AppSettings settings = SettingsClient.Instance.AppSettings;
            settings.ModificationKey = key;
#endif
        }
    }
}
