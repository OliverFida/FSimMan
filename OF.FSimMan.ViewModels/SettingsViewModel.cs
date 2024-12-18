using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games;
using OF.FSimMan.Management.Games.Fs;
using OF.FSimMan.ViewModel.Base;

namespace OF.FSimMan.ViewModel
{
    public class SettingsViewModel : RememberableBusyViewModelBase
    {
        #region Properties
        public AppSettings AppSettings => ((SettingsClient)Client).AppSettings;

        public AppSettingsGameFs22? GameSettingsFs22
        {
            get => AppSettings?.GetGameSettings<AppSettingsGameFs22>();
        }

        public AppSettingsGameFs25? GameSettingsFs25
        {
            get => AppSettings?.GetGameSettings<AppSettingsGameFs25>();
        }

        public bool IsFs25Visible
        {
            get => ReleaseFeatures.GameFs25 && ((SettingsClient)Client).AppSettings.GetGameSettings<AppSettingsGameFs25>().IsEnabled;
        }
        #endregion

        #region Constructor
        public SettingsViewModel() : base(SettingsClient.Instance)
        {
            ((SettingsClient)Client).AppSettings.GetGameSettings<AppSettingsGameFs25>().PropertyChanged += AppSettingsChanged;
        }
        #endregion

        #region Methods PRIVATE
        private void AppSettingsChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is null || !e.PropertyName.Equals(nameof(AppSettingsGameBase.IsEnabled))) return;

            OnPropertyChanged(nameof(IsFs25Visible));
        }
        #endregion
    }
}
