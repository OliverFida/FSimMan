using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Management;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public class Fs25EditModPackViewModel : FsEditModPackViewModelBase
    {
        #region Constructor
        public Fs25EditModPackViewModel(EditMode editMode) : base(new Fs25EditModPackClient(), editMode) { }
        #endregion
    }
}
