namespace OliverFida.Base
{
    public class ClientBase : ObjectBase, IClientBase
    {
        private bool _isBusy = false;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private string _busyContent = string.Empty;
        public string BusyContent
        {
            get => _busyContent;
            set => SetProperty(ref _busyContent, value);
        }

        public ClientBase()
        {
            ResetBusyIndicator();
        }

        public void ResetBusyIndicator()
        {
            IsBusy = false;
            BusyContent = "Please wait...";
        }
    }
}
