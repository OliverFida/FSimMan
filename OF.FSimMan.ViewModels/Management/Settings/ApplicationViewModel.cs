using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games.Fs;

namespace OF.FSimMan.ViewModel.Management.Settings
{
    public class ApplicationViewModel : BusyViewModelBase
    {
        #region Properties
        public override bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

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

        #region Commands
        public Command CheckModiKeyCommand { get; }
        private void CheckModiKeyDelegate()
        {
            try
            {
                ((SettingsClient)Client).AppSettings.CheckModiKey();
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public ApplicationViewModel() : base("Application", SettingsClient.Instance)
        {
            CheckModiKeyCommand = new Command(this, target => ExecuteBusy(((ApplicationViewModel)target).CheckModiKeyDelegate));
        }
        #endregion
    }
}
