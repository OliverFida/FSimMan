using OF.Base.ViewModel;

namespace OliverFida.FSimMan.ViewModels.UI.DialogWindow
{
    public class WarningViewModel : ViewModelBase
    {
        public string WarningMessage { get; }

        public WarningViewModel(string warningMessage)
        {
            WarningMessage = warningMessage;
        }
    }
}
