using OF.Base.ViewModel;
using OF.FSimMan.Client.Management.Settings;

namespace OF.FSimMan.ViewModel.Management.Settings
{
    public class ChangeLogViewModel : BusyViewModelBase
    {
        #region Constructor
        public ChangeLogViewModel() : base("Change Log", ChangeLogClient.Instance) { }
        #endregion
    }
}
