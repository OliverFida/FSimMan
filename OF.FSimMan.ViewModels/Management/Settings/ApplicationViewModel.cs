using OF.Base.ViewModel;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games.Fs;

namespace OF.FSimMan.ViewModel.Management.Settings
{
    public class ApplicationViewModel : BusyViewModelBase
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
            get => ReleaseFeatures.GameFs25;
        }
        #endregion

        #region Constructor
        public ApplicationViewModel() : base("Application", SettingsClient.Instance) { }
        #endregion
    }
}
