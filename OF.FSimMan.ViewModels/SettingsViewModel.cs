using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;
using OF.FSimMan.ViewModel.Base;

namespace OF.FSimMan.ViewModel
{
    public class SettingsViewModel : RememberableBusyViewModelBase
    {
        #region Properties
        public AppSettings AppSettings => ((SettingsClient)Client).AppSettings;

        public bool IsFs25Visible
        {
            get => ReleaseFeatures.GameFs25 && ((SettingsClient)Client).AppSettings.IsFs25Active;
        }
        #endregion

        #region Constructor
        public SettingsViewModel() : base(SettingsClient.Instance)
        {
            ((SettingsClient)Client).AppSettings.PropertyChanged += AppSettingsChanged;
        }
        #endregion

        #region Methods PRIVATE
        private void AppSettingsChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            SettingsClient client = (SettingsClient)Client;
            if (e.PropertyName is null || !e.PropertyName.Equals(nameof(client.AppSettings.IsFs25Active))) return;

            OnPropertyChanged(nameof(IsFs25Visible));
        }
        #endregion
    }
}
