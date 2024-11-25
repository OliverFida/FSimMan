using OF.Base.Objects;
using OF.Base.ViewModel;
using OliverFida.FSimMan.Config;
using OliverFida.FSimMan.UI;
using OliverFida.FSimMan.ViewModels.Modpack;

namespace OliverFida.FSimMan.ViewModels.UI
{
    class AppBarViewModel : ViewModelBase
    {
        public static bool IsFs25Visible
        {
            get => ReleaseFeatures.GameFs25;
        }
        public AppSettings? AppSettings
        {
            get => CurrentApplication.AppSettings;
        }

        public string VersionText
        {
            get => CurrentApplication.AssemblyVersionText;
        }

        public bool IsAppBarEnabled
        {
            get
            {
                Type? viewModel = MainWindow.ViewModelSelector.CurrentViewModel?.GetType();
                if (viewModel == null ||
                    viewModel == typeof(EditModpackViewModel) ||
                    viewModel == typeof(GameRunningViewModel)) return false;

                return true;
            }
        }

        public bool IsUpdateAvailable
        {
            get => CurrentApplication.IsUpdateAvailable;
        }

        public AppBarViewModel()
        {
            MainWindow.ViewModelSelector.ActiveViewModelChangedEvent += HandleActiveViewModelChanged;
            CurrentApplication.IsUpdateAvailableChanged += HandleIsUpdateAvailableChanged;

            UpdateCommand = new Command(this, async target => await ((AppBarViewModel)target).UpdateDelegate());
        }

        private void HandleActiveViewModelChanged(object? sender, ActiveViewModelChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsHomeSelected));
            OnPropertyChanged(nameof(IsFs22Selected));
            OnPropertyChanged(nameof(IsFs25Selected));
            OnPropertyChanged(nameof(IsSettingsSelected));

            OnPropertyChanged(nameof(IsAppBarEnabled));
        }

        private void HandleIsUpdateAvailableChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(IsUpdateAvailable));
        }

        public Command UpdateCommand { get; }
        private async Task UpdateDelegate()
        {
            await MainWindow.ExecuteUpdate();
        }

        public bool IsDebugVisible
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }
        public Command DebugCommand { get; } = new Command(DebugDelegate);
        private static void DebugDelegate()
        {
#if DEBUG
            string message = "Das ist ein wundervoller Text, der sicher nicht über das Maximum des DialogWindows hinüberragt.\r\n\r\nWirklich ein toller Text.\r\nKaum zu glauben.\r\nEin echtes Meisterwerk!";
            UiFunctions.ShowError(message);
            UiFunctions.ShowWarningOk(message);
            UiFunctions.ShowWarningOkCancel(message);
            UiFunctions.ShowInfoOk(message);
            UiFunctions.ShowInfoOkCancel(message);
            UiFunctions.ShowQuestion(message);
#endif
        }

        public bool IsHomeSelected
        {
            get
            {
                IViewModel? vm = MainWindow.ViewModelSelector.CurrentViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(HomeViewModel));
            }
        }
        public Command HomeCommand { get; } = new Command(HomeDelegate);
        private static void HomeDelegate()
        {
            try
            {
                MainWindow.ViewModelSelector.OpenViewModel(MainWindow.HomeViewModel);
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        private static Fs22ViewModel? _fs22ViewModel;
        public bool IsFs22Selected
        {
            get
            {
                IViewModel? vm = MainWindow.ViewModelSelector.CurrentViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(Fs22ViewModel));
            }
        }
        public Command SelectFs22Command { get; } = new Command(SelectFs22Delegate);
        private static void SelectFs22Delegate()
        {
            try
            {
                _fs22ViewModel = new Fs22ViewModel();
                MainWindow.ViewModelSelector.OpenViewModel(_fs22ViewModel);
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        public Command RunFs22DefaultCommand { get; } = new Command(RunFs22DefaultDelegate);
        private static async void RunFs22DefaultDelegate()
        {
            await Task.Run(() =>
            {
                try
                {
                    _fs22ViewModel!.Client.RunGame(null);
                    GameRunningViewModel runningViewModel = new GameRunningViewModel(SupportedGame.Fs22);
                    MainWindow.ViewModelSelector.OpenViewModel(runningViewModel);
                }
                catch (OfException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        private static Fs25ViewModel? _fs25ViewModel;
        public bool IsFs25Selected
        {
            get
            {
                IViewModel? vm = MainWindow.ViewModelSelector.CurrentViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(Fs25ViewModel));
            }
        }
        public Command SelectFs25Command { get; } = new Command(SelectFs25Delegate);
        private static void SelectFs25Delegate()
        {
            try
            {
                _fs25ViewModel = new Fs25ViewModel();
                MainWindow.ViewModelSelector.OpenViewModel(_fs25ViewModel);
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        public Command RunFs25DefaultCommand { get; } = new Command(RunFs25DefaultDelegate);
        private static async void RunFs25DefaultDelegate()
        {
            await Task.Run(() =>
            {
                try
                {
                    _fs25ViewModel!.Client.RunGame(null);
                    GameRunningViewModel runningViewModel = new GameRunningViewModel(SupportedGame.Fs25);
                    MainWindow.ViewModelSelector.OpenViewModel(runningViewModel);
                }
                catch (OfException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public bool IsSettingsSelected
        {
            get
            {
                IViewModel? vm = MainWindow.ViewModelSelector.CurrentViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(SettingsViewModel));
            }
        }
        public Command SettingsCommand { get; } = new Command(SettingsDelegate);
        private static void SettingsDelegate()
        {
            try
            {
                MainWindow.ViewModelSelector.OpenViewModel(MainWindow.SettingsViewModel);
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
    }
}
