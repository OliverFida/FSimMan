using CLS.Core;
using CLS.Core.ViewModel;
using OF.FSimMan.Updater.Clients;

namespace OF.FSimMan.Updater.ViewModels
{
    public class MainViewModel : ViewModelBase, ISingleton<MainViewModel>
    {
        #region Properties
        public static UpdateClient UpdateClient
        {
            get => UpdateClient.Instance;
        }

        private static ViewModelSelector _viewModelSelector = new ViewModelSelector();
        public static ViewModelSelector ViewModelSelector
        {
            get => _viewModelSelector;
        }
        #endregion

        #region Constructor
        private MainViewModel() : base("FSimMan Updater")
        {

        }

        protected override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            ViewModelSelector.OpenViewModel(new SummaryViewModel());
            await UpdateClient.CheckUpdateAvailableAsync();
        }
        #endregion

        #region ISingleton
        private static MainViewModel _instance = new MainViewModel();
        public static MainViewModel Instance
        {
            get => _instance;
        }
        #endregion
    }
}
