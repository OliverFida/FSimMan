using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Management;
using OliverFida.FSimMan.Exceptions;

namespace OF.FSimMan.ViewModel
{
    public class MainViewModel : ViewModelBase, ISingleton<MainViewModel>
    {
        #region Properties
        public CurrentApplication CurrentApplication { get => CurrentApplication.Instance; }

        public static ViewModelSelector ViewModelSelector { get; } = new ViewModelSelector();

        public static HomeViewModel HomeViewModel { get; } = new HomeViewModel();
        #endregion

        #region Events
        public event EventHandler? UpdateCompleteEvent;
        #endregion

        #region Constructor & Initialize
        private MainViewModel() : base(true) { }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            try
            {
                ViewModelSelector.OpenViewModel(HomeViewModel);
#if !DEBUG
                await AutoUpdateAsync();
#endif
            }
            catch (Exception ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Methods PUBLIC
        public async Task ExecuteUpdate()
        {
            try
            {
                UpdateClient updateClient = UpdateClient.Instance;
                if (!await updateClient.TryExecuteUpdateAsync())
                {
                    UiFunctions.ShowWarningOk("Update failed!" + Environment.NewLine + "Please try again later.");
                    return;
                }

                UpdateCompleteEvent?.Invoke(this, EventArgs.Empty);
            }
            catch (UpdateCanceledException ex)
            {
                UiFunctions.ShowInfoOk(ex.Message);
            }
        }
        #endregion

        #region Methods PRIVATE
        private async Task AutoUpdateAsync()
        {
            UpdateClient updateClient = UpdateClient.Instance;
            await updateClient.CheckUpdateAvailableAsync();

            if (!updateClient.IsUpdateAvailable || !UiFunctions.ShowQuestion("A new version of FSimMan is available!" + Environment.NewLine + Environment.NewLine + "Would you like to update now?")) return;

            await ExecuteUpdate();
        }
        #endregion

        #region ISingleton
        private static readonly MainViewModel _instance = new MainViewModel();
        public static MainViewModel Instance => _instance;
        #endregion
    }
}
