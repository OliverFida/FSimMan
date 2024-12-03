using Microsoft.Win32;
using OF.Base.Objects;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Game;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Game;
using OF.FSimMan.Management;
using OF.FSimMan.ViewModel.Base;

namespace OF.FSimMan.ViewModel.Game
{
    public abstract class GameViewModelBase : RememberableBusyViewModelBase
    {
        #region Properties
        public abstract bool IsOpenable { get; }

        public static AppSettings AppSettings { get => SettingsClient.Instance.AppSettings; }
        public static bool IsModPackImportExportVisible { get => ReleaseFeatures.ModPackImportExport; }

        protected EditModPackViewModelBase? _editModPackViewModel;
        #endregion

        #region Commands
        public Command NewModPackCommand { get; }
        protected abstract void NewModPackDelegate();

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

                IGameClient client = (IGameClient)Client;
                bool importAsNew = false;
                bool alreadyExists = client.GetModPackExists(fileDialog.FileName);

                if (alreadyExists) importAsNew = !UiFunctions.ShowQuestion("A modpack with the same key already exists!\r\nWould you like to overwrite?");
                client.ImportModPack(fileDialog.FileName, importAsNew);
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        public Command PlayModpackCommand { get; }
        private void PlayModpackDelegate()
        {
            try
            {
                if (PlayModpackCommand.Parameter == null) return;
                ModPack modPack = (ModPack)PlayModpackCommand.Parameter;

                RunGameOnClientInitializeComplete(modPack);
                GameRunningViewModel gameRunningViewModel = new GameRunningViewModel(this);
                MainViewModel.ViewModelSelector.OpenViewModel(gameRunningViewModel);
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        public virtual bool IsPlayModpackEnabled
        {
            get => true;
        }

        public Command EditModpackCommand { get; }
        protected abstract void EditModpackDelegate();

        public Command ExportModpackCommand { get; }
        private void ExportModpackDelegate()
        {
            try
            {
                if (ExportModpackCommand.Parameter == null) return;
                ModPack modPack = (ModPack)ExportModpackCommand.Parameter;

                if (!UiFunctions.ShowWarningOkCancel("FSimMan is exporting ALL you mods!\r\nPlease be aware to not act against copyright and distribution laws!\r\nFSimMan and it's developers are NOT responsible for any violations!")) return;

                string fileName = $"{((IGameClient)Client).Game.ToString().ToLower()}_{modPack.Title.Replace(" ", "")}";
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

                ((IGameClient)Client).ExportModPack(modPack, fileDialog.FileName);
            }
            catch (OfException ex)
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
                ModPack modPack = (ModPack)DeleteModpackCommand.Parameter;

                if (!UiFunctions.ShowQuestion($@"Are you sure you want to delete the modpack ""{modPack.Title}""?")) return;

                ((IGameClient)Client).DeleteModPack(modPack);
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public GameViewModelBase(IGameClient client) : base(client)
        {
            NewModPackCommand = new Command(this, target => ExecuteBusy(() => ExecutePreventAutoclose(((GameViewModelBase)target).NewModPackDelegate)));
            ImportModPackCommand = new Command(this, target => ExecuteBusy(((GameViewModelBase)target).ImportModPackDelegate));
            PlayModpackCommand = new Command(this, target => ExecuteBusy(((GameViewModelBase)target).PlayModpackDelegate));
            EditModpackCommand = new Command(this, target => ExecuteBusy(() => ExecutePreventAutoclose(((GameViewModelBase)target).EditModpackDelegate)));
            ExportModpackCommand = new Command(this, target => ExecuteBusy(((GameViewModelBase)target).ExportModpackDelegate));
            DeleteModpackCommand = new Command(this, target => ExecuteBusy(((GameViewModelBase)target).DeleteModpackDelegate));
        }
        #endregion

        #region Methods PUBLIC
        public void RunGameOnClientInitializeComplete(ModPack? modPack)
        {
            IGameClient client = (IGameClient)Client;
            client.SelectedModPack = modPack;

            if (Client.IsInitialized)
            {
                client.RunGame();
            }
            else
            {
                Client.InitializeComplete += HandleClientInizializeCompleteRunGame;
            }
        }
        #endregion

        #region Methods PROTECTED
        protected void HandleEditModPackViewModelClosedEvent(object? sender, EventArgs e)
        {
            ((IGameClient)Client).RefreshModPacks();
            _editModPackViewModel!.ViewModelClosedEvent -= HandleEditModPackViewModelClosedEvent;
        }
        #endregion

        #region Methods PRIVATE
        private void HandleClientInizializeCompleteRunGame(object? sender, EventArgs e)
        {
            Client.InitializeComplete -= HandleClientInizializeCompleteRunGame;
            ((IGameClient)Client).RunGame();
        }
        #endregion
    }
}
