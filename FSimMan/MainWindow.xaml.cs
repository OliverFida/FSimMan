using OliverFida.Base;
using OliverFida.FSimMan.Client;
using OliverFida.FSimMan.UI;
using OliverFida.FSimMan.ViewModels;

namespace OliverFida.FSimMan
{
    public partial class MainWindow : UI.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            try
            {
                CurrentApplication.Initialize(AppSettingsClient.GetSettings());
            }
            catch (Exception ex)
            {
                UiFunctions.ShowError(ex);
            }

            ViewModelSelector.SetActiveViewModel(HomeViewModel);
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