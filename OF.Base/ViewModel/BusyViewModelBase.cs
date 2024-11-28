using OF.Base.Client;
using OF.Base.Objects;

namespace OF.Base.ViewModel
{
    public abstract class BusyViewModelBase : ViewModelBase, IBusyViewModel
    {
        #region Properties
        private readonly IClient _client;
        public IClient Client => _client;
        #endregion

        #region Constructor
        public BusyViewModelBase(IClient client)
        {
            _client = client;

            Client.PropertyChanged += ClientPropertyChanged;
        }

        private void ClientPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(IBusyIndicatorManager.IsBusy):
                    OnPropertyChanged(nameof(IsBusy));
                    break;
                case nameof(IBusyIndicatorManager.BusyContent):
                    OnPropertyChanged(nameof(BusyContent));
                    break;
            }
        }
        #endregion

        #region Methods PUBLIC
        public void ExecuteBusy(Action action)
        {
            Task.Run(() =>
            {
                try
                {
                    IsBusy = true;

                    action.Invoke();
                }
                finally
                {
                    ResetBusyIndicator();
                }
            });
        }
        #endregion

        #region IBusyIndicatorManager
        public virtual bool IsBusy
        {
            get => Client.IsBusy;
            set => Client.IsBusy = value;
        }

        public string BusyContent {
            get => Client.BusyContent;
            set => Client.BusyContent = value;
        }

        public void ResetBusyIndicator()
        {
            Client.ResetBusyIndicator();
        }
        #endregion
    }
}
