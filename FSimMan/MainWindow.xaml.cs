using OliverFida.Base;
using OliverFida.FSimMan.Client;
using OliverFida.FSimMan.UI;
using OliverFida.FSimMan.ViewModels;
using System.Windows;

namespace OliverFida.FSimMan
{
    public partial class MainWindow : UI.Window
    {
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

            ViewModelSelector.SetActiveViewModel(HomeViewModel);
        }

        private async Task InitializeAsync()
        {
#if !DEBUG
            await Application.Current.Dispatcher.BeginInvoke(AutoUpdateAsync);
#endif
        }

        private async Task AutoUpdateAsync()
        {
            UpdateClient updateClient = UpdateClient.Instance;
            bool isUpdateAvailable = await updateClient.GetUpdateAvailableAsync();

            if (!isUpdateAvailable || !UiFunctions.ShowQuestion("A new version of FSimMan is available!" + Environment.NewLine + Environment.NewLine + "Would you like to update now?")) return;

            if (!await updateClient.TryExecuteUpdateAsync())
            {
                UiFunctions.ShowWarningOk("Update failed!" + Environment.NewLine + "Please try again later.");
                return;
            }
            Application.Current.Shutdown(0);
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