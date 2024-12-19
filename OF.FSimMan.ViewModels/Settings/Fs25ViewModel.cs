using OF.FSimMan.Client.Management;

namespace OF.FSimMan.ViewModel.Settings
{
    public class Fs25ViewModel : GameViewModelBase
    {
        #region Constructor
        public Fs25ViewModel() : base(Management.Game.FarmingSim25, SettingsClient.Instance) { }
        #endregion
    }
}
