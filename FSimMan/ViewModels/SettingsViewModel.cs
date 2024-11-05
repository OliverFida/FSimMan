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

        public SettingsViewModel()
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            SaveSettingsCommand = new Command(this, target => ((SettingsViewModel)target).SaveSettingsDelegate());
            SelectFs22GamePathCommand = new Command(this, target => ((SettingsViewModel)target).SelectFs22GamePathDelegate());
            SelectFs22DataPathCommand= new Command(this, target => ((SettingsViewModel)target).SelectFs22DataPathDelegate());
            SelectFs25GamePathCommand = new Command(this, target => ((SettingsViewModel)target).SelectFs25GamePathDelegate());
            SelectFs25DataPathCommand= new Command(this, target => ((SettingsViewModel)target).SelectFs25DataPathDelegate());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }

        public Command SaveSettingsCommand { get; }
        private async Task SaveSettingsDelegate()
        {
            await Task.Run(() =>
            {
                try
                {
                    //Client.IsBusy = true;

                    if (CurrentApplication.AppSettings == null) return;
                    AppSettingsClient.StoreSettings(CurrentApplication.AppSettings);
                }
                catch (OFException ex)
                {
                    UiFunctions.ShowError(ex);
                }
                //finally
                //{
                //    Client.ResetBusyIndicator();
                //}
            });
        }

        #region FS22
        public Command SelectFs22GamePathCommand { get; }
        private async Task SelectFs22GamePathDelegate()
        {
            await Task.Run(() =>
            {
                try
                {
                    //Client.IsBusy = true;

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
                //finally
                //{
                //    Client.ResetBusyIndicator();
                //}
            });
        }

        public Command SelectFs22DataPathCommand { get; }
        private async Task SelectFs22DataPathDelegate()
        {
            await Task.Run(() =>
            {
                try
                {
                    //Client.IsBusy = true;

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
                //finally
                //{
                //    Client.ResetBusyIndicator();
                //}
            });
        }
        #endregion

        #region FS25
        public Command SelectFs25GamePathCommand { get; }
        private async Task SelectFs25GamePathDelegate()
        {
            await Task.Run(() =>
            {
                try
                {
                    //Client.IsBusy = true;

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
                //finally
                //{
                //    Client.ResetBusyIndicator();
                //}
            });
        }

        public Command SelectFs25DataPathCommand { get; }
        private async Task SelectFs25DataPathDelegate()
        {
            await Task.Run(() =>
            {
                try
                {
                    //Client.IsBusy = true;

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
                //finally
                //{
                //    Client.ResetBusyIndicator();
                //}
            });
        }
        #endregion
    }
}
