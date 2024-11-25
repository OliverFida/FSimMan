using OF.Base.ViewModel;
using OF.FSimMan.Client.Management;
using OliverFida.FSimMan.Exceptions;

namespace OF.FSimMan.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        public static CurrentApplication CurrentApplication { get => CurrentApplication.Instance; }

        public static ViewModelSelector ViewModelSelector { get; } = new ViewModelSelector();

        public static HomeViewModel HomeViewModel { get; } = new HomeViewModel();
        #endregion

        #region Constructor & Initialize
        public MainViewModel() : base(true) { }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            // OFDO: TryCatch
            ViewModelSelector.OpenViewModel(HomeViewModel);
            await AutoUpdateAsync();
        }
        #endregion

        #region Methods PUBLIC
        public static async Task ExecuteUpdate()
        {
            try
            {
                UpdateClient updateClient = UpdateClient.Instance;
                if (!await updateClient.TryExecuteUpdateAsync())
                {
                    // OFDO: UiFunctions.ShowWarningOk("Update failed!" + Environment.NewLine + "Please try again later.");
                    return;
                }
                // OFDO: Application.Current.Shutdown(0);
            }
            catch (UpdateCanceledException ex)
            {
                // OFDO: UiFunctions.ShowInfoOk(ex.Message);
            }
        }
        #endregion

        #region Methods PRIVATE
        private async Task AutoUpdateAsync()
        {
            UpdateClient updateClient = UpdateClient.Instance;
            await updateClient.CheckUpdateAvailableAsync();

            // OFDO: if (!updateClient.IsUpdateAvailable || !UiFunctions.ShowQuestion("A new version of FSimMan is available!" + Environment.NewLine + Environment.NewLine + "Would you like to update now?")) return;

            //await ExecuteUpdate();
        }
        #endregion
    }
}
