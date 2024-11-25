using OF.Base.ViewModel;
using OliverFida.FSimMan.Client;
using OliverFida.FSimMan.Exceptions;
using OliverFida.FSimMan.UI;
using OliverFida.FSimMan.ViewModels;
using System.Windows;

namespace OliverFida.FSimMan
{
    public partial class MainWindow : UI.Window
    {
        public static UpdateClient UpdateClient { get => UpdateClient.Instance; }

        public MainWindow()
        {
            InitializeComponent();
            try
            {
                Task.Run(InitializeAsync);
                CurrentApplication.Initialize(AppSettingsClient.GetSettings());
            }
            catch (Exception ex)
            {
                UiFunctions.ShowError(ex);
            }

            ViewModelSelector.OpenViewModel(HomeViewModel);
        }

        public static async Task ExecuteUpdate()
        {
            try
            {
                if (!await UpdateClient.TryExecuteUpdateAsync())
                {
                    UiFunctions.ShowWarningOk("Update failed!" + Environment.NewLine + "Please try again later.");
                    return;
                }
                Application.Current.Shutdown(0);
            }
            catch (UpdateCanceledException ex)
            {
                UiFunctions.ShowInfoOk(ex.Message);
            }
        }

        private async Task InitializeAsync()
        {
#if !DEBUG
            await Application.Current.Dispatcher.BeginInvoke(AutoUpdateAsync);
#else
            await Task.Delay(100);
#endif
        }

        private async Task AutoUpdateAsync()
        {
            CurrentApplication.IsUpdateAvailable = await UpdateClient.GetUpdateAvailableAsync();

            if (!CurrentApplication.IsUpdateAvailable || !UiFunctions.ShowQuestion("A new version of FSimMan is available!" + Environment.NewLine + Environment.NewLine + "Would you like to update now?")) return;

            await ExecuteUpdate();
        }

        public static string AppTitle
        {
            get => CurrentApplication.AppTitle;
        }

        internal static HomeViewModel HomeViewModel { get; } = new HomeViewModel();
        internal static SettingsViewModel SettingsViewModel { get; } = new SettingsViewModel();
        public static ViewModelSelector ViewModelSelector { get; } = new ViewModelSelector();
    }
}