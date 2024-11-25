﻿using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;
using OF.FSimMan.ViewModel;
using OF.FSimMan.ViewModel.Game.Fs;
using System.ComponentModel;

namespace OF.FSimMan.UI
{
    public class AppBarViewModel : ViewModelBase
    {
        #region Properties
        public string VersionText
        {
            get => CurrentApplication.AssemblyVersionText;
        }

        public AppSettings? AppSettings
        {
            get => SettingsClient.Instance.AppSettings;
        }

        public bool IsAppBarEnabled
        {
            get
            {
                Type? viewModelType = MainViewModel.ViewModelSelector.CurrentViewModel?.GetType();
                // OFDO: if(viewModelType == null ||
                //    viewModelType == typeof(EditMod)) {

                return true;
            }
        }

        public static Fs22ViewModel? Fs22ViewModel { get; private set; }
        public static Fs25ViewModel? Fs25ViewModel { get; private set; }
        #endregion

        #region Commands
        public Command UpdateCommand { get; }
        private async Task UpdateDelegate()
        {
            await MainViewModel.ExecuteUpdate();
        }
        public bool IsUpdateAvailable
        {
            get => UpdateClient.Instance.IsUpdateAvailable;
        }

        public Command HomeCommand { get; } = new Command(HomeDelegate);
        private static void HomeDelegate()
        {
            try
            {
                MainViewModel.ViewModelSelector.OpenViewModel(MainViewModel.HomeViewModel);
            }
            catch (OfException ex)
            {
                // OFDO: UiFunctions.ShowError(ex);
            }
        }
        public bool IsHomeSelected
        {
            get
            {
                IViewModel? vm = MainViewModel.ViewModelSelector.CurrentViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(HomeViewModel));
            }
        }

        public Command SelectFs22Command { get; } = new Command(SelectFs22Delegate);
        private static void SelectFs22Delegate()
        {
            try
            {
                Fs22ViewModel = new Fs22ViewModel();
                MainViewModel.ViewModelSelector.OpenViewModel(Fs22ViewModel!);
            }
            catch (OfException ex)
            {
                // OFDO: UiFunctions.ShowError(ex);
            }
        }
        public Command RunFs22DefaultCommand { get; } = new Command(RunFs22DefaultDelegate);
        private static async void RunFs22DefaultDelegate()
        {
            await Task.Run(() =>
            {
                try
                {
                    // OFDO: _fs22ViewModel!.Client.RunGame(null);
                    //GameRunningViewModel runningViewModel = new GameRunningViewModel(SupportedGame.Fs22);
                    //MainViewModel.ViewModelSelector.SetActiveViewModel(runningViewModel);
                }
                catch (OfException ex)
                {
                    // OFDO: UiFunctions.ShowError(ex);
                }
            });
        }
        public bool IsFs22Selected
        {
            get
            {
                IViewModel? vm = MainViewModel.ViewModelSelector.CurrentViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(Fs22ViewModel));
            }
        }

        public Command SelectFs25Command { get; } = new Command(SelectFs25Delegate);
        private static void SelectFs25Delegate()
        {
            try
            {
                Fs25ViewModel = new Fs25ViewModel();
                MainViewModel.ViewModelSelector.OpenViewModel(Fs25ViewModel!);
            }
            catch (OfException ex)
            {
                // OFDO: UiFunctions.ShowError(ex);
            }
        }
        public Command RunFs25DefaultCommand { get; } = new Command(RunFs25DefaultDelegate);
        private static async void RunFs25DefaultDelegate()
        {
            await Task.Run(() =>
            {
                try
                {
                    // OFDO: _fs25ViewModel!.Client.RunGame(null);
                    //GameRunningViewModel runningViewModel = new GameRunningViewModel(SupportedGame.Fs25);
                    //MainWindow.ViewModelSelector.SetActiveViewModel(runningViewModel);
                }
                catch (OfException ex)
                {
                    // OFDO: UiFunctions.ShowError(ex);
                }
            });
        }
        public static bool IsFs25Visible
        {
            get => ReleaseFeatures.GameFs25;
        }
        public bool IsFs25Selected
        {
            get
            {
                IViewModel? vm = MainViewModel.ViewModelSelector.CurrentViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(Fs25ViewModel));
            }
        }

        public Command SettingsCommand { get; } = new Command(SettingsDelegate);
        private static void SettingsDelegate()
        {
            try
            {
                MainViewModel.ViewModelSelector.OpenViewModel(new SettingsViewModel());
            }
            catch (OfException ex)
            {
                // OFDO: UiFunctions.ShowError(ex);
            }
        }
        public bool IsSettingsSelected
        {
            get
            {
                IViewModel? vm = MainViewModel.ViewModelSelector.CurrentViewModel;
                if (vm == null) return false;

                return vm.GetType().Equals(typeof(SettingsViewModel));
            }
        }
        #endregion

        #region Constructor
        public AppBarViewModel() : base(true)
        {
            MainViewModel.ViewModelSelector.PropertyChanged += HandleActiveViewModelChanged;
            UpdateClient.Instance.PropertyChanged += HandleIsUpdateAvailableChanged;

            UpdateCommand = new Command(this, async target => await ((AppBarViewModel)target).UpdateDelegate());
        }
        #endregion

        #region Methods PRIVATE
        private void HandleActiveViewModelChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(MainViewModel.ViewModelSelector.CurrentViewModel)) return;

            OnPropertyChanged(nameof(IsAppBarEnabled));

            OnPropertyChanged(nameof(IsHomeSelected));
            OnPropertyChanged(nameof(IsFs22Selected));
            OnPropertyChanged(nameof(IsFs25Selected));
            OnPropertyChanged(nameof(IsSettingsSelected));
        }

        private void HandleIsUpdateAvailableChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(UpdateClient.IsUpdateAvailable)) return;

            OnPropertyChanged(nameof(IsUpdateAvailable));
        }
        #endregion

        #region DEBUG
        public Command DebugCommand { get; } = new Command(DebugDelegate);
        private static void DebugDelegate()
        {
#if DEBUG
            //string message = "Das ist ein wundervoller Text, der sicher nicht über das Maximum des DialogWindows hinüberragt.\r\n\r\nWirklich ein toller Text.\r\nKaum zu glauben.\r\nEin echtes Meisterwerk!";
            // OFDO: UiFunctions.ShowError(message);
            //UiFunctions.ShowWarningOk(message);
            //UiFunctions.ShowWarningOkCancel(message);
            //UiFunctions.ShowInfoOk(message);
            //UiFunctions.ShowInfoOkCancel(message);
            //UiFunctions.ShowQuestion(message);
#endif
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
        #endregion
    }
}