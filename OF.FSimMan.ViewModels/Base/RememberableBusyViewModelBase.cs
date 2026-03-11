using CLS.Core.Client;

namespace OF.FSimMan.ViewModel.Base
{
    public abstract class RememberableBusyViewModelBase : CLS.Core.ViewModel.BusyViewModelBase, IRememberableViewModel
    {
        #region Constructor
        public RememberableBusyViewModelBase(string title, IClient client) : base(title, client) { }
        #endregion
    }
}
