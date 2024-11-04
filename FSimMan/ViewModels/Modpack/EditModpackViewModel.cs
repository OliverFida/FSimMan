using Microsoft.Win32;
using OliverFida.Base;
using OliverFida.FSimMan.Client;
using OliverFida.FSimMan.Client.ModPack;
using OliverFida.FSimMan.UI;
using System.IO;

namespace OliverFida.FSimMan.ViewModels.Modpack
{
    internal class EditModpackViewModel : ViewModelBase
    {
        #region Properties
        private FsBaseClient _fsClient;

        private EditModPackClient _editModPackClient;
        public EditModPackClient Client { get => _editModPackClient; }
        #endregion

        #region Constructor
        public EditModpackViewModel(FsBaseClient fsBaseClient, Config.ModPack.ModPack modPack, EditMode editMode = EditMode.Edit)
        {
            _fsClient = fsBaseClient;
            _editModPackClient = new EditModPackClient(fsBaseClient, modPack, editMode);

            ExitCommand = new Command(this, target => ((EditModpackViewModel)target).ExitDelegate());
            SaveCommand = new Command(this, target => ((EditModpackViewModel)target).SaveDelegate());
            AddModCommand = new Command(this, target => ((EditModpackViewModel)target).AddModDelegate());
            DeleteModCommand = new Command(this, target => ((EditModpackViewModel)target).DeleteModDelegate());
        }
        #endregion

        #region Commands
        public Command ExitCommand { get; }
        private void ExitDelegate()
        {
            try
            {
                if (Client.ModPack.IsModified || Client.EditMode == EditMode.New)
                {
                    if (!UiFunctions.ShowQuestion("Exit without saving?")) return;
                }
                Client.CancelEditing();

                _fsClient.RefreshModPacks();
                MainWindow.ViewModelSelector.CloseCurrentViewModel();
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command SaveCommand { get; }
        private void SaveDelegate()
        {
            try
            {
                Client.StoreModPack();
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command AddModCommand { get; }
        private void AddModDelegate()
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog()
                {
                    InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"),
                    Filter = "zip archives (*.zip)|*.zip",
                    Title = "Select mod .zip",
                    DefaultExt = "zip",
                    Multiselect = true
                };
                bool? result = fileDialog.ShowDialog();
                if (result != true) return;

                Client.ModPack.AddMods(fileDialog.FileNames);
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command DeleteModCommand { get; }
        private void DeleteModDelegate()
        {
            try
            {
                if (DeleteModCommand.Parameter == null) return;
                Config.ModPack.Mod mod = (Config.ModPack.Mod)DeleteModCommand.Parameter;

                if (!UiFunctions.ShowQuestion($@"Are you sure you want to remove the mod ""{mod.Title}""?")) return;

                Client.ModPack.DeleteMod(mod);
            }
            catch (OFException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion
    }
}
