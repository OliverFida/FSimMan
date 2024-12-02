using OF.Base.ViewModel;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games.Fs;

namespace OF.FSimMan.ViewModel.Settings
{
    public class ApplicationViewModel : BusyViewModelBase
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
            get => ReleaseFeatures.GameFs25;
        }
        #endregion

        #region Constructor
        public ApplicationViewModel() : base(SettingsClient.Instance) { }
        #endregion
    }
}
