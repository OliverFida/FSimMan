using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Game;
using OF.FSimMan.Management;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public class Fs22EditModPackViewModel : FsEditModPackViewModelBase
    {
        #region Constructor
        public Fs22EditModPackViewModel(EditMode editMode, ModPack modPack, Fs22Client gameClient) : base(new Fs22EditModPackClient(modPack, gameClient), editMode) { }
        #endregion
    }
}
