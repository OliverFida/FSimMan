using OF.Base.ViewModel;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;

namespace OF.FSimMan.ViewModel.Settings
{
    public class ApplicationViewModel : BusyViewModelBase
    {
        #region Properties
        public AppSettings AppSettings => ((SettingsClient)Client).AppSettings;

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
