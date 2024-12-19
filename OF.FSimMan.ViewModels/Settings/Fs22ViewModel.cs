using OF.FSimMan.Client.Management;

namespace OF.FSimMan.ViewModel.Settings
{
    public class Fs22ViewModel : GameViewModelBase
    {
        #region Constructor
        public Fs22ViewModel() : base(Management.Game.FarmingSim22, SettingsClient.Instance) { }
        #endregion
    }
}
