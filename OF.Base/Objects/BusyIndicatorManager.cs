namespace OF.Base.Objects
{
    public abstract class BusyIndicatorManager : BindingObject, IBusyIndicatorManager
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

        protected BusyIndicatorManager()
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
