using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Management;
using OF.FSimMan.ViewModel.Base;
using OF.FSimMan.ViewModel.Game;
using OliverFida.FSimMan.Exceptions;
using System.Reflection;
using System.Windows;

namespace OF.FSimMan.ViewModel
{
    public class MainViewModel : ViewModelBase, ISingleton<MainViewModel>
    {
        #region Properties
        public WindowState StartupWindowState
        {
            get
            {
#if DEBUG
                return WindowState.Normal;
#else
                return WindowState.Maximized;
#endif
            }
        }

        public CurrentApplication CurrentApplication { get => CurrentApplication.Instance; }

        public static ViewModelSelector ViewModelSelector { get; } = new ViewModelSelector();

        public static HomeViewModel HomeViewModel { get; } = new HomeViewModel();
        #endregion

        #region Events
        public event EventHandler? UpdateCompleteEvent;
        #endregion

        #region Constructor & Initialize
        private MainViewModel() : base("Main", true) { }

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            try
            {
                OpenLastView();
#if !DEBUG
                await AutoUpdateAsync();
                //await ShowChangelogAsync();
#endif
                GameRunningViewModel temp = GameRunningViewModel.Instance; // Triggering constructor
            }
            catch (OfException ex)
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

        //private async Task ShowChangelogAsync()
        //{
        //    UpdateClient updateClient = UpdateClient.Instance;
        //    if (!await updateClient.GetShowChangelogAsync()) return;

        //    SettingsViewModel.OpenChangeLog();
        //}

        private void OpenLastView()
        {
            ViewModelSelector.CurrentViewModelChanged += HandleCurrentViewModelChanged;
            string lastSelectedView = SettingsClient.Instance.AppSettings.LastSelectedView;
            if (!string.IsNullOrWhiteSpace(lastSelectedView))
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                Type? lastType = assembly.GetType(lastSelectedView);
                if (lastType is not null)
                {
                    IViewModel? viewModel = (IViewModel?)Activator.CreateInstance(lastType);
                    if (viewModel is not null) ViewModelSelector.OpenViewModel(viewModel);
                    return;
                }
            }
            ViewModelSelector.OpenViewModel(HomeViewModel);
            // OFDOL: OpenAvailableView();
        }

        // OFDOL: private void OpenAvailableView()
        //{
        //    if (SettingsClient.Instance.AppSettings.IsFs25Active) ViewModelSelector.OpenViewModel(new Fs25ViewModel());
        //    else if (SettingsClient.Instance.AppSettings.IsFs22Active) ViewModelSelector.OpenViewModel(new Fs22ViewModel());
        //    else ViewModelSelector.OpenViewModel(new SettingsViewModel());
        //}

        private void HandleCurrentViewModelChanged(object? sender, EventArgs e)
        {
            if (ViewModelSelector.CurrentViewModel == null) return;

            if (ViewModelSelector.CurrentViewModel.GetType().IsAssignableTo(typeof(GameViewModelBase)) &&
                !((GameViewModelBase)ViewModelSelector.CurrentViewModel).IsOpenable) ViewModelSelector.OpenViewModel(HomeViewModel);

            if (ViewModelSelector.CurrentViewModel.GetType().IsAssignableTo(typeof(IRememberableViewModel))) SettingsClient.Instance.AppSettings.LastSelectedView = ViewModelSelector.CurrentViewModel.GetType().ToString();
        }
        #endregion

        #region ISingleton
        private static readonly MainViewModel _instance = new MainViewModel();
        public static MainViewModel Instance => _instance;
        #endregion
    }
}
