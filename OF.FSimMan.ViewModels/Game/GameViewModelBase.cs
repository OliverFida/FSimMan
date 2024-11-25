using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.FSimMan.Client.Game;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;

namespace OF.FSimMan.ViewModel.Game
{
    public abstract class GameViewModelBase : ViewModelBase
    {
        #region Properties
        protected IGameClient _client;
        public IGameClient Client
        {
            get => _client;
        }

        public static AppSettings AppSettings { get => SettingsClient.Instance.AppSettings; }
        public static bool IsModPackImportExportVisible { get => ReleaseFeatures.ModPackImportExport; }
        #endregion

        #region Commands
        public Command NewModPackCommand { get; }
        private void NewModPackDelegate()
        {
            try
            {
                // OFDO: Config.ModPack.ModPack? modPack = Client.BeginNewModPack();
                //if (modPack == null) return;

                //EditModpackViewModel editViewModel = new EditModpackViewModel(Client, modPack, EditMode.New);
                //MainViewModel.ViewModelSelector.OpenViewModel(editViewModel);
            }
            catch (OfException ex)
            {
                // OFDO: UiFunctions.ShowError(ex);
            }
        }

        public Command ImportModPackCommand { get; }
        private void ImportModPackDelegate()
        {
            try
            {
                // OFDO: OpenFileDialog fileDialog = new OpenFileDialog()
                //{
                //    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                //    Filter = "Modpack files (*.fsmmp)|*.fsmmp",
                //    Title = "Select modpack to import",
                //    DefaultExt = "fsmmp"
                //};
                //bool? result = fileDialog.ShowDialog();
                //if (result != true) return;

                //using (FsmmpFile fsmmpFile = new FsmmpFile(fileDialog.FileName))
                //{
                //    bool alreadyExists = Client.ImportCheckModPackExists(fsmmpFile);
                //    if (alreadyExists && !UiFunctions.ShowQuestion("A modpack with the same key already exists!\r\nWould you like to overwrite?")) return;
                //    Client.ImportModPack(fsmmpFile, alreadyExists);
                //}
            }
            catch (OfException ex)
            {
                // OFOD: UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public GameViewModelBase(IGameClient client)
        {
            _client = client;

            NewModPackCommand = new Command(this, target => ((GameViewModelBase)target).NewModPackDelegate());
            ImportModPackCommand = new Command(this, target => ((GameViewModelBase)target).ImportModPackDelegate());
        }
        #endregion
    }
}
