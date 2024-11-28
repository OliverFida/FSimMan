using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Game;
using OF.FSimMan.Management;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public class Fs25EditModPackViewModel : FsEditModPackViewModelBase
    {
        #region Constructor
        public Fs25EditModPackViewModel(EditMode editMode, ModPack modPack) : base(new Fs25EditModPackClient(modPack), editMode) { }
        #endregion
    }
}
