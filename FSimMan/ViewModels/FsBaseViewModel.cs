using Microsoft.Win32;
using OliverFida.Base;
using OliverFida.FSimMan.Client;
using OliverFida.FSimMan.Config;
using OliverFida.FSimMan.ImportExport;
using OliverFida.FSimMan.UI;
using OliverFida.FSimMan.ViewModels.Modpack;
using System.IO;

namespace OliverFida.FSimMan.ViewModels
{
    public abstract class FsBaseViewModel : GameBaseViewModel
    {
        #region Properties
        public static bool IsModPackImportExportVisible
        {
            get => ReleaseFeatures.ModPackImportExport;
        }
        public static bool IsModPackHubVisible
        {
            get => ReleaseFeatures.ModPackHub;
        }

        public static AppSettings? AppSettings
        {
            get => CurrentApplication.AppSettings;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        private FsBaseClient _client;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public FsBaseClient Client
        {
            get => _client;
        }
        #endregion

        #region Constructor
        public FsBaseViewModel(FsEdition fsEdition) : base(fsEdition)
        {
            switch (fsEdition)
            {
                case FsEdition.Fs22:
                    _client = new Fs22Client();
                    break;
                case FsEdition.Fs25:
                    _client = new Fs25Client();
                    break;
                default:
                    throw new NotImplementedException();
            }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            NewModPackCommand = new Command(this, target => ((FsBaseViewModel)target).NewModPackDelegate());
            ImportModPackCommand = new Command(this, target => ((FsBaseViewModel)target).ImportModPackDelegate());
            PlayModpackCommand = new Command(this, target => ((FsBaseViewModel)target).PlayModpackDelegate());
            EditModpackCommand = new Command(this, target => ((FsBaseViewModel)target).EditModpackDelegate());
            ExportModpackCommand = new Command(this, target => ((FsBaseViewModel)target).ExportModpackDelegate());
            DeleteModpackCommand = new Command(this, target => ((FsBaseViewModel)target).DeleteModpackDelegate());
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
        }
        #endregion

        #region Commands
        public Command NewModPackCommand { get; }
        private void NewModPackDelegate()
        {
            try
            {
                Config.ModPack.ModPack? modPack = Client.BeginNewModPack();
                if (modPack == null) return;

                EditModpackViewModel editViewModel = new EditModpackViewModel(Client, modPack, EditMode.New);
                MainWindow.ViewModelSelector.SetActiveViewModel(editViewModel);
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command ImportModPackCommand { get; }
        private void ImportModPackDelegate()
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Filter = "Modpack files (*.fsmmp)|*.fsmmp",
                    Title = "Select modpack to import",
                    DefaultExt = "fsmmp"
                };
                bool? result = fileDialog.ShowDialog();
                if (result != true) return;

                using (FsmmpFile fsmmpFile = new FsmmpFile(fileDialog.FileName))
                {
                    bool alreadyExists = Client.ImportCheckModPackExists(fsmmpFile);
                    if (alreadyExists && !UiFunctions.ShowQuestion("A modpack with the same key already exists!\r\nWould you like to overwrite?")) return;
                    Client.ImportModPack(fsmmpFile, alreadyExists);
                }
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command PlayModpackCommand { get; }
        private async Task PlayModpackDelegate()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (PlayModpackCommand.Parameter == null) return;
                    Config.ModPack.ModPack modPack = (Config.ModPack.ModPack)PlayModpackCommand.Parameter;

                    Client.RunGame(modPack);
                    GameRunningViewModel runningViewModel = new GameRunningViewModel(Game);
                    MainWindow.ViewModelSelector.SetActiveViewModel(runningViewModel);
                }
                catch (OFException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command EditModpackCommand { get; }
        private void EditModpackDelegate()
        {
            try
            {
                if (EditModpackCommand.Parameter == null) return;
                Config.ModPack.ModPack modPack = (Config.ModPack.ModPack)EditModpackCommand.Parameter;

                EditModpackViewModel editViewModel = new EditModpackViewModel(Client, modPack);
                MainWindow.ViewModelSelector.SetActiveViewModel(editViewModel);
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command ExportModpackCommand { get; }
        private void ExportModpackDelegate()
        {
            try
            {
                if (ExportModpackCommand.Parameter == null) return;
                Config.ModPack.ModPack modPack = (Config.ModPack.ModPack)ExportModpackCommand.Parameter;

                if (!UiFunctions.ShowWarningOkCancel("FSimMan is exporting ALL you mods!\r\nPlease be aware to not act against copyright and distribution laws!\r\nFSimMan and it's developers are NOT responsible for any violations!")) return;

                string fileName = $"{_client.FsEdition.ToString().ToLower()}_{modPack.Title.Replace(" ", "")}";
                if (!string.IsNullOrWhiteSpace(modPack.Version)) fileName += $"_v{modPack.Version}";
                fileName += ".fsmmp";

                SaveFileDialog fileDialog = new SaveFileDialog()
                {
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Filter = "Modpack files (*.fsmmp)|*.fsmmp",
                    Title = "Save modpack",
                    DefaultExt = "fsmmp",
                    FileName = fileName
                };
                bool? result = fileDialog.ShowDialog();
                if (result != true) return;

                Client.ExportModPack(modPack, fileDialog.FileName);
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command DeleteModpackCommand { get; }
        private void DeleteModpackDelegate()
        {
            try
            {
                if (DeleteModpackCommand.Parameter == null) return;
                Config.ModPack.ModPack modPack = (Config.ModPack.ModPack)DeleteModpackCommand.Parameter;

                if (!UiFunctions.ShowQuestion($@"Are you sure you want to delete the modpack ""{modPack.Title}""?")) return;

                Client.DeleteModPack(modPack);
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion
    }
}
