using Microsoft.Win32;
using OliverFida.Base;
using OliverFida.FSimMan.Client;
using OliverFida.FSimMan.Config;
using OliverFida.FSimMan.UI;

namespace OliverFida.FSimMan.ViewModels
{
    class SettingsViewModel : ViewModelBase
    {
        public static bool IsFs25Visible
        {
            get => ReleaseFeatures.GameFs25;
        }

        public AppSettings? AppSettings
        {
            get
            {
                return CurrentApplication.AppSettings;
            }
        }

        public Command SaveSettingsCommand { get; } = new Command(SaveSettingsDelegate);
        private static void SaveSettingsDelegate()
        {
            try
            {
                if (CurrentApplication.AppSettings == null) return;
                AppSettingsClient.StoreSettings(CurrentApplication.AppSettings);
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        private static AboutViewModel? _aboutViewModel;
        public Command ShowAboutCommand { get; } = new Command(ShowAboutDelegate);
        private static void ShowAboutDelegate()
        {
            try
            {
                _aboutViewModel = new AboutViewModel();
                MainWindow.ViewModelSelector.SetActiveViewModel(_aboutViewModel);
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        #region FS22
        public Command SelectFs22GamePathCommand { get; } = new Command(SelectFs22GamePathDelegate);
        private static void SelectFs22GamePathDelegate()
        {
            try
            {
                if (CurrentApplication.AppSettings == null) return;

                OpenFolderDialog dialog = new OpenFolderDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                    Title = "Select FS22 Installation Folder",
                    Multiselect = false
                };
                if (dialog.ShowDialog() != true) return;

                CurrentApplication.AppSettings.Fs22GamePath = dialog.FolderName;
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command SelectFs22DataPathCommand { get; } = new Command(SelectFs22DataPathDelegate);
        private static void SelectFs22DataPathDelegate()
        {
            try
            {
                if (CurrentApplication.AppSettings == null) return;

                OpenFolderDialog dialog = new OpenFolderDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Title = "Select FS22 Data Folder",
                    Multiselect = false
                };
                if (dialog.ShowDialog() != true) return;

                CurrentApplication.AppSettings.Fs22DataPath = dialog.FolderName;
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region FS25
        public Command SelectFs25GamePathCommand { get; } = new Command(SelectFs25GamePathDelegate);
        private static void SelectFs25GamePathDelegate()
        {
            try
            {
                if (CurrentApplication.AppSettings == null) return;

                OpenFolderDialog dialog = new OpenFolderDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                    Title = "Select FS25 Installation Folder",
                    Multiselect = false
                };
                if (dialog.ShowDialog() != true) return;

                CurrentApplication.AppSettings.Fs25GamePath = dialog.FolderName;
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command SelectFs25DataPathCommand { get; } = new Command(SelectFs25DataPathDelegate);
        private static void SelectFs25DataPathDelegate()
        {
            try
            {
                if (CurrentApplication.AppSettings == null) return;

                OpenFolderDialog dialog = new OpenFolderDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Title = "Select FS25 Data Folder",
                    Multiselect = false
                };
                if (dialog.ShowDialog() != true) return;

                CurrentApplication.AppSettings.Fs25DataPath = dialog.FolderName;
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion
    }
}
