using OliverFida.Base;
using OliverFida.FSimMan.Client;
using OliverFida.FSimMan.Config;
using OliverFida.FSimMan.UI;
using OliverFida.FSimMan.ViewModels.Modpack;

namespace OliverFida.FSimMan.ViewModels
{
    public abstract class FsBaseViewModel : GameBaseViewModel
    {
        #region Properties
        public static bool IsModPackImportVisible
        {
            get => ReleaseFeatures.ModPackImport;
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
            PlayModpackCommand = new Command(this, target => ((FsBaseViewModel)target).PlayModpackDelegate());
            EditModpackCommand = new Command(this, target => ((FsBaseViewModel)target).EditModpackDelegate());
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
