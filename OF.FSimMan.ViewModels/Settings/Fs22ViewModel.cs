using Microsoft.Win32;
using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games.Fs;

namespace OF.FSimMan.ViewModel.Settings
{
    public class Fs22ViewModel : BusyViewModelBase
    {
        #region Properties
        public AppSettings AppSettings => ((SettingsClient)Client).AppSettings;

        public AppSettingsGameFs22? GameSettingsFs22
        {
            get => AppSettings?.GetGameSettings<AppSettingsGameFs22>();
        }
        #endregion

        #region Commands
        public Command SelectFs22GamePathCommand { get; }
        private void SelectFs22GamePathDelegate()
        {
            try
            {
                OpenFolderDialog dialog = new OpenFolderDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                    Title = "Select FS22 Installation Folder",
                    Multiselect = false
                };
                if (dialog.ShowDialog() != true) return;

                ((SettingsClient)Client).AppSettings.GetGameSettings<AppSettingsGameFs22>().ExeDirectoryPath = dialog.FolderName;
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command SelectFs22DataPathCommand { get; }
        private void SelectFs22DataPathDelegate()
        {
            try
            {
                OpenFolderDialog dialog = new OpenFolderDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Title = "Select FS22 Data Folder",
                    Multiselect = false
                };
                if (dialog.ShowDialog() != true) return;

                ((SettingsClient)Client).AppSettings.GetGameSettings<AppSettingsGameFs22>().DataDirectoryPath = dialog.FolderName;
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public Fs22ViewModel() : base(SettingsClient.Instance)
        {
            SelectFs22GamePathCommand = new Command(this, target => ExecuteBusy(((Fs22ViewModel)target).SelectFs22GamePathDelegate));
            SelectFs22DataPathCommand = new Command(this, target => ExecuteBusy(((Fs22ViewModel)target).SelectFs22DataPathDelegate));
        }
        #endregion
    }
}
