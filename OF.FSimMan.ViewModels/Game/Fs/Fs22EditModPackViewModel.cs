using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Management;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public class Fs22EditModPackViewModel : FsEditModPackViewModelBase
    {
        #region Constructor
        public Fs22EditModPackViewModel(EditMode editMode) : base(new Fs22EditModPackClient(), editMode) { }
        #endregion
    }
}
