using OF.Base.Client;

namespace OF.FSimMan.ViewModel.Base
{
    public abstract class RememberableBusyViewModelBase : OF.Base.ViewModel.BusyViewModelBase, IRememberableViewModel
    {
        #region Constructor
        public RememberableBusyViewModelBase(string title, IClient client) : base(title, client) { }
        #endregion
    }
}
