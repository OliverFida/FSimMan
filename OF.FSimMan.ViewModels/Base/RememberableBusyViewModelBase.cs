using OF.Base.Client;

namespace OF.FSimMan.ViewModel.Base
{
    public abstract class RememberableBusyViewModelBase : OF.Base.ViewModel.BusyViewModelBase, IRememberableViewModel
    {
        #region Constructor
        public RememberableBusyViewModelBase(IClient client) : base(client) { }
        #endregion
    }
}
