using OF.Base.ViewModel;

namespace OF.Base.Wpf.UiFunctions
{
    public class InfoViewModel : ViewModelBase
    {
        public string InfoMessage { get; }

        public InfoViewModel(string infoMessage)
        {
            InfoMessage = infoMessage;
        }
    }
}
