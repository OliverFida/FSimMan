using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Game;
using OF.FSimMan.Management;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public class Fs25EditModPackViewModel : EditModPackViewModelBase
    {
        #region Constructor
        public Fs25EditModPackViewModel(EditMode editMode, ModPack modPack, Fs25Client gameClient) : base(modPack.Title, new Fs25EditModPackClient(modPack, gameClient), editMode) { }
        #endregion
    }
}
