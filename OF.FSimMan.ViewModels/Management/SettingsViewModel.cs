using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games;
using OF.FSimMan.Management.Games.Fs;
using OF.FSimMan.ViewModel.Base;

namespace OF.FSimMan.ViewModel.Management
{
    public class SettingsViewModel : RememberableBusyViewModelBase
    {
        #region Properties
        public AppSettings AppSettings => ((SettingsClient)Client).AppSettings;

        public GameSettingsFs22? GameSettingsFs22
        {
            get => AppSettings?.GetGameSettings<GameSettingsFs22>();
        }

        public GameSettingsFs25? GameSettingsFs25
        {
            get => AppSettings?.GetGameSettings<GameSettingsFs25>();
        }

        public bool IsFs25Visible
        {
            get => ReleaseFeatures.GameFs25 && ((SettingsClient)Client).AppSettings.GetGameSettings<GameSettingsFs25>().IsEnabled;
        }
        #endregion

        #region Constructor
        public SettingsViewModel() : base("Settings", SettingsClient.Instance)
        {
            ((SettingsClient)Client).AppSettings.GetGameSettings<GameSettingsFs25>().PropertyChanged += AppSettingsChanged;
        }
        #endregion

        #region Methods PUBLIC
        //public static void OpenChangeLog()
        //{
        //    SettingsViewModel settingsViewModel = new SettingsViewModel();
        //    MainViewModel.ViewModelSelector.OpenViewModel(settingsViewModel);

        //    //OFDOI: Show ChangeLogTab
        //}
        #endregion

        #region Methods PRIVATE
        private void AppSettingsChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName is null || !e.PropertyName.Equals(nameof(GameSettingsBase.IsEnabled))) return;

            OnPropertyChanged(nameof(IsFs25Visible));
        }
        #endregion
    }
}
