using OF.FSimMan.Client.Management;

namespace OF.FSimMan.ViewModel.Management.Settings
{
    public class Fs22ViewModel : GameViewModelBase
    {
        #region Constructor
        public Fs22ViewModel() : base("Farming Simulator 22", FSimMan.Management.Game.FarmingSim22, SettingsClient.Instance) { }
        #endregion
    }
}
