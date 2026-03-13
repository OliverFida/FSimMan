using CLS.Core;
using CLS.Core.ViewModel.Command;
using CLS.Core.Wpf.UiFunctions;
using Microsoft.Win32;
using OF.FSimMan.Client.Game;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Game;
using OF.FSimMan.Management;
using OF.FSimMan.ViewModel.Base;
using System.Diagnostics;

namespace OF.FSimMan.ViewModel.Game
{
    public abstract class GameViewModelBase : RememberableBusyViewModelBase
    {
        #region Properties
        public static bool IsModPackHubVisible { get => ReleaseFeatures.GiantsModPackHub; }

        public abstract bool IsOpenable { get; }

        public static AppSettings AppSettings { get => SettingsClient.Instance.AppSettings; }
        public static bool IsModPackImportVisible { get => ReleaseFeatures.ModPackImportExport; }

        protected EditModPackViewModelBase? _editModPackViewModel;
        #endregion

        #region Commands
        public Command RefreshModPacksCommand { get; }
        private Task RefreshModPacksDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    ((IGameClient)Client).RefreshModPacks();
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command NewModPackCommand { get; }
        protected abstract Task NewModPackDelegate();

        public Command ImportModPackCommand { get; }
        private Task ImportModPackDelegate()
        {
            return AsTask(() =>
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
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command PlayModpackCommand { get; }
        private Task PlayModpackDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    if (PlayModpackCommand.Parameter == null) return;
                    ModPack modPack = (ModPack)PlayModpackCommand.Parameter;

                    GameRunningViewModel.Instance.PlanStart(((GameClientBase)Client).Game);
                    RunGameOnClientInitializeComplete(modPack);
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }
        public virtual bool IsPlayModpackEnabled
        {
            get => true;
        }

        public Command EditModpackCommand { get; }
        protected abstract Task EditModpackDelegate();

        public Command ExportModpackCommand { get; }
        private Task ExportModpackDelegate()
        {
            return AsTask(() =>
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
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command DeleteModpackCommand { get; }
        private Task DeleteModpackDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    if (DeleteModpackCommand.Parameter == null) return;
                    ModPack modPack = (ModPack)DeleteModpackCommand.Parameter;

                    if (!UiFunctions.ShowQuestion($@"Are you sure you want to delete the modpack ""{modPack.Title}""?")) return;

                    ((IGameClient)Client).DeleteModPack(modPack);
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }
        #endregion

        #region Constructor
        public GameViewModelBase(string title, IGameClient client) : base(title, client)
        {
            RefreshModPacksCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).RefreshModPacksDelegate));
            NewModPackCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).NewModPackDelegate));
            ImportModPackCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).ImportModPackDelegate, false));
            PlayModpackCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).PlayModpackDelegate));
            EditModpackCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).EditModpackDelegate));
            ExportModpackCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).ExportModpackDelegate, false));
            DeleteModpackCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).DeleteModpackDelegate, false));
        }
        #endregion

        #region Methods PUBLIC
        public void RunGameOnClientInitializeComplete(ModPack? modPack)
        {
            IGameClient client = (IGameClient)Client;
            client.SelectedModPack = modPack;

            if (!Client.IsInitialized) Client.WaitForInitializationComplete();
            client.RunGame();
        }
        #endregion

        #region Methods PROTECTED
        protected void HandleEditModPackViewModelClosedEvent(object? sender, EventArgs e)
        {
            ((IGameClient)Client).RefreshModPacks();
            _editModPackViewModel!.ViewModelClosedEvent -= HandleEditModPackViewModelClosedEvent;
        }
        #endregion
    }
}
