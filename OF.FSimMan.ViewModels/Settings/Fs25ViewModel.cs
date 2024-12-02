using Microsoft.Win32;
using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;

namespace OF.FSimMan.ViewModel.Settings
{
    public class Fs25ViewModel : BusyViewModelBase
    {
        #region Properties
        public AppSettings AppSettings => ((SettingsClient)Client).AppSettings;
        #endregion

        #region Commands
        public Command SelectFs25GamePathCommand { get; }
        private void SelectFs25GamePathDelegate()
        {
            try
            {
                OpenFolderDialog dialog = new OpenFolderDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                    Title = "Select FS25 Installation Folder",
                    Multiselect = false
                };
                if (dialog.ShowDialog() != true) return;

                ((SettingsClient)Client).AppSettings.Fs25GamePath = dialog.FolderName;
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command SelectFs25DataPathCommand { get; }
        private void SelectFs25DataPathDelegate()
        {
            try
            {
                OpenFolderDialog dialog = new OpenFolderDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                    Title = "Select FS25 Data Folder",
                    Multiselect = false
                };
                if (dialog.ShowDialog() != true) return;

                ((SettingsClient)Client).AppSettings.Fs25DataPath = dialog.FolderName;
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public Fs25ViewModel() : base(SettingsClient.Instance)
        {
            SelectFs25GamePathCommand = new Command(this, target => ExecuteBusy(((Fs25ViewModel)target).SelectFs25GamePathDelegate));
            SelectFs25DataPathCommand = new Command(this, target => ExecuteBusy(((Fs25ViewModel)target).SelectFs25DataPathDelegate));
        }
        #endregion
    }
}
