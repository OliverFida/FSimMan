﻿using OliverFida.Base;
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


        private static Fs22ViewModel? _fs22ViewModel;
        private static Fs22ViewModel Fs22ViewModel
        {
            get
            {
                if (_fs22ViewModel != null) _fs22ViewModel.Client.RefreshModPacks();
                if (_fs22ViewModel == null) _fs22ViewModel = new Fs22ViewModel();
                return _fs22ViewModel;
            }
        }

        private static Fs25ViewModel? _fs25ViewModel;
        private static Fs25ViewModel Fs25ViewModel
        {
            get
            {
                if (_fs25ViewModel != null) _fs25ViewModel.Client.RefreshModPacks();
                if (_fs25ViewModel == null) _fs25ViewModel = new Fs25ViewModel();
                return _fs25ViewModel;
            }
        }

        public bool IsAppBarEnabled
        {
            get
            {
                Type? viewModel = MainWindow.ViewModelSelector.ActiveViewModel?.GetType();
                if (viewModel == null ||
                    viewModel == typeof(EditModpackViewModel) ||
                    viewModel == typeof(GameRunningViewModel)) return false;

                return true;
            }
        }

        public AppBarViewModel()
        {
            MainWindow.ViewModelSelector.ActiveViewModelChangedEvent += HandleActiveViewModelChanged;
        }

        private void HandleActiveViewModelChanged(object? sender, ActiveViewModelChangedEventArgs e)
        {
            OnPropertyChanged(nameof(IsHomeSelected));
            OnPropertyChanged(nameof(IsFs22Selected));
            OnPropertyChanged(nameof(IsFs25Selected));
            OnPropertyChanged(nameof(IsSettingsSelected));

            OnPropertyChanged(nameof(IsAppBarEnabled));
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
            UiFunctions.ShowError("Test Error");
            UiFunctions.ShowWarningOk("Test Warning");
            UiFunctions.ShowWarningOkCancel("Test Warning");
            UiFunctions.ShowInfoOk("Test Info");
            UiFunctions.ShowInfoOkCancel("Test Info");
            UiFunctions.ShowQuestion("Test Question");
#endif
        }

        public bool IsHomeSelected
        {
            get
            {
                ViewModelBase? vm = MainWindow.ViewModelSelector.ActiveViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(HomeViewModel));
            }
        }
        public Command HomeCommand { get; } = new Command(HomeDelegate);
        private static void HomeDelegate()
        {
            try
            {
                MainWindow.ViewModelSelector.SetActiveViewModel(MainWindow.HomeViewModel);
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public bool IsFs22Selected
        {
            get
            {
                ViewModelBase? vm = MainWindow.ViewModelSelector.ActiveViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(Fs22ViewModel));
            }
        }
        public Command SelectFs22Command { get; } = new Command(SelectFs22Delegate);
        private static void SelectFs22Delegate()
        {
            try
            {
                MainWindow.ViewModelSelector.SetActiveViewModel(Fs22ViewModel);
            }
            catch (OFException ex)
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
                    Fs22ViewModel.Client.RunGame(null);
                    GameRunningViewModel runningViewModel = new GameRunningViewModel(SupportedGame.Fs22);
                    MainWindow.ViewModelSelector.SetActiveViewModel(runningViewModel);
                }
                catch (OFException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public bool IsFs25Selected
        {
            get
            {
                ViewModelBase? vm = MainWindow.ViewModelSelector.ActiveViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(Fs25ViewModel));
            }
        }
        public Command SelectFs25Command { get; } = new Command(SelectFs25Delegate);
        private static void SelectFs25Delegate()
        {
            try
            {
                MainWindow.ViewModelSelector.SetActiveViewModel(Fs25ViewModel);
            }
            catch (OFException ex)
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
                    Fs25ViewModel.Client.RunGame(null);
                    GameRunningViewModel runningViewModel = new GameRunningViewModel(SupportedGame.Fs25);
                    MainWindow.ViewModelSelector.SetActiveViewModel(runningViewModel);
                }
                catch (OFException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public bool IsSettingsSelected
        {
            get
            {
                ViewModelBase? vm = MainWindow.ViewModelSelector.ActiveViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(SettingsViewModel));
            }
        }
        public Command SettingsCommand { get; } = new Command(SettingsDelegate);
        private static void SettingsDelegate()
        {
            try
            {
                MainWindow.ViewModelSelector.SetActiveViewModel(MainWindow.SettingsViewModel);
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
    }
}