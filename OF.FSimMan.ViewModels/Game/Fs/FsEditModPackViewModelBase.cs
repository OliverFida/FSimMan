using OF.Base.Client;
using OF.FSimMan.Management;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public abstract class FsEditModPackViewModelBase : EditModPackViewModelBase
    {
        #region Constructor
        public FsEditModPackViewModelBase(IClient client, EditMode editMode) : base(client, editMode) { }
        #endregion
    }
}
