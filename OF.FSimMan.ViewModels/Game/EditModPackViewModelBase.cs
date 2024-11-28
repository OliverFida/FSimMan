using OF.Base.Client;
using OF.Base.ViewModel;
using OF.FSimMan.Management;

namespace OF.FSimMan.ViewModel.Game
{
    public abstract class EditModPackViewModelBase : BusyViewModelBase
    {
        #region Constructor
        public EditModPackViewModelBase(IClient client, EditMode editMode) : base(client) { }
        #endregion
    }
}
