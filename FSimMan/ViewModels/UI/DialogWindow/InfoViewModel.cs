using OliverFida.Base;

namespace OliverFida.FSimMan.ViewModels.UI.DialogWindow
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
