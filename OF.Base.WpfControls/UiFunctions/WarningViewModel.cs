using OF.Base.ViewModel;

namespace OF.Base.Wpf.UiFunctions
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
