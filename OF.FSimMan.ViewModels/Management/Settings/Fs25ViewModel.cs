using OF.FSimMan.Client.Management;

namespace OF.FSimMan.ViewModel.Management.Settings
{
    public class Fs25ViewModel : GameViewModelBase
    {
        #region Constructor
        public Fs25ViewModel() : base("Farming Simulator 25", FSimMan.Management.Game.FarmingSim25, SettingsClient.Instance) { }
        #endregion
    }
}
