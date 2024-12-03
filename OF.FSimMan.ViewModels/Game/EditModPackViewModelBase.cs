using Microsoft.Win32;
using OF.Base.Client;
using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Game;
using OF.FSimMan.Game;
using OF.FSimMan.Management;
using System.IO;
using System.Windows;

namespace OF.FSimMan.ViewModel.Game
{
    public abstract class EditModPackViewModelBase : BusyViewModelBase
    {
        #region Properties
        private EditMode _editMode;
        #endregion

        #region Commands
        public Command ExitCommand { get; }
        private void ExitDelegate()
        {
            try
            {
                EditModPackClientBase client = (EditModPackClientBase)Client;
                if (client.ModPack.IsModified || _editMode == EditMode.New)
                {
                    if (!UiFunctions.ShowQuestion("Exit without saving?")) return;
                }
                Application.Current.Dispatcher.Invoke(client.CancelEdit);
                MainViewModel.ViewModelSelector.CloseCurrentViewModel();
                InvokeViewModelClosedEvent();
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command SaveCommand { get; }
        private void SaveDelegate()
        {
            try
            {
                ((EditModPackClientBase)Client).StoreModPack();
                _editMode = EditMode.Edit;
            }
            catch (OfException ex)
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

                Application.Current.Dispatcher.Invoke(() => ((EditModPackClientBase)Client).AddMods(fileDialog.FileNames));
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command RemoveModCommand { get; }
        private void RemoveModDelegate()
        {
            try
            {
                if (RemoveModCommand.Parameter == null) return;
                Mod mod = (Mod)RemoveModCommand.Parameter;

                if (!UiFunctions.ShowQuestion($@"Are you sure you want to remove the mod ""{mod.Title}""?")) return;

                Application.Current.Dispatcher.Invoke(() => ((EditModPackClientBase)Client).RemoveMod(mod));
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public EditModPackViewModelBase(IClient client, EditMode editMode) : base(client)
        {
            _editMode = editMode;

            ExitCommand = new Command(this, target => ExecuteBusy(((EditModPackViewModelBase)target).ExitDelegate));
            SaveCommand = new Command(this, target => ExecuteBusy(((EditModPackViewModelBase)target).SaveDelegate));
            AddModCommand = new Command(this, target => ExecuteBusy(((EditModPackViewModelBase)target).AddModDelegate));
            RemoveModCommand = new Command(this, target => ExecuteBusy(((EditModPackViewModelBase)target).RemoveModDelegate));
        }
        #endregion
    }
}
