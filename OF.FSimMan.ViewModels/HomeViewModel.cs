using OF.FSimMan.Client;
using OF.FSimMan.ViewModel.Base;

namespace OF.FSimMan.ViewModel
{
    public class HomeViewModel : RememberableBusyViewModelBase
    {
        #region Constructor
        public HomeViewModel() : base("Home", new HomeClient(), true) { }
        #endregion
    }
}
