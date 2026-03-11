using Microsoft.Win32;
using CLS.Core.Client;
using CLS.Core.ViewModel;
using OF.FSimMan.Client.Game;
using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games;
using System.IO;
using CLS.Core.ViewModel.Command;
using CLS.Core;
using CLS.Core.Wpf.UiFunctions;

namespace OF.FSimMan.ViewModel.Management.Settings
{
    public abstract class GameViewModelBase : BusyViewModelBase
    {
        #region Properties
        private FSimMan.Management.Game _game;

        public override bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        public AppSettings AppSettings => ((SettingsClient)Client).AppSettings;

        public GameSettingsBase GameSettings
        {
            get => AppSettings.GetGameSettings(_game);
        }
        #endregion

        #region Commands
        public Command SelectGamePathCommand { get; }
        private Task SelectGamePathDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    OpenFolderDialog dialog = new OpenFolderDialog()
                    {
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86),
                        Title = "Select Installation Folder",
                        Multiselect = false
                    };
                    if (dialog.ShowDialog() != true) return;

                    ((SettingsClient)Client).AppSettings.GetGameSettings(_game).ValidateExeDirectoryPath(dialog.FolderName);
                    ((SettingsClient)Client).AppSettings.GetGameSettings(_game).ExeDirectoryPath = dialog.FolderName;
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command AutodetectGamePathCommand { get; }
        private Task AutodetectGamePathDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    string busyContentBefore = BusyContent;
                    try
                    {
                        BusyContent = "Autodetection running...";
                        ((SettingsClient)Client).AppSettings.GetGameSettings(_game).TryAutodetectExePath();
                    }
                    catch (WarningAsException ex)
                    {
                        BusyContent = busyContentBefore;
                        UiFunctions.ShowWarningOk(ex.Message);

                        SelectGamePathDelegate();
                    }
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command SelectDataPathCommand { get; }
        private Task SelectDataPathDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    OpenFolderDialog dialog = new OpenFolderDialog()
                    {
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                        Title = "Select Data Folder",
                        Multiselect = false
                    };
                    if (dialog.ShowDialog() != true) return;

                    ((SettingsClient)Client).AppSettings.GetGameSettings(_game).ValidateDataDirectoryPath(dialog.FolderName);
                    ((SettingsClient)Client).AppSettings.GetGameSettings(_game).DataDirectoryPath = dialog.FolderName;
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }

        public Command AutodetectDataPathCommand { get; }
        private Task AutodetectDataPathDelegate()
        {
            return AsTask(() =>
            {
                try
                {
                    string busyContentBefore = BusyContent;
                    try
                    {
                        BusyContent = "Autodetection running...";
                        Thread.Sleep(1000);
                        ((SettingsClient)Client).AppSettings.GetGameSettings(_game).TryAutodetectDataPath();
                    }
                    catch (WarningAsException ex)
                    {
                        BusyContent = busyContentBefore;
                        UiFunctions.ShowWarningOk(ex.Message);

                        SelectDataPathDelegate();
                    }
                }
                catch (ClsException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }
        #endregion

        #region Constructor
        protected GameViewModelBase(string title, FSimMan.Management.Game game, IClient client) : base(title, client)
        {
            _game = game;
            SelectGamePathCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).SelectGamePathDelegate, false));
            AutodetectGamePathCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).AutodetectGamePathDelegate, false));
            SelectDataPathCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).SelectDataPathDelegate, false));
            AutodetectDataPathCommand = new Command(this, target => ExecuteDelegate(((GameViewModelBase)target).AutodetectDataPathDelegate, false));

            AppSettings.ModPackAutogenerationNowPossible += HandleAppSettingsModPackAutogenerationNowPossible;
        }
        #endregion

        #region Methods PRIVATE
        private void HandleAppSettingsModPackAutogenerationNowPossible(object? sender, AppSettingsModPackAutogenerationNowPossibleEventArgs e)
        {
            ExecuteDelegate(() =>
            {
                return AsTask(() =>
                {
                    try
                    {
                        if (!e.Game.Equals(_game)) return;

                        GameSettingsBase gameSettings = AppSettings.GetGameSettings(_game);

                        string modsDirectoryPath = Path.Combine(SettingsClient.Instance.AppSettings.GetGameSettings(_game).DataDirectoryPath, "mods");
                        DirectoryInfo modsDirectoryInfo = new DirectoryInfo(modsDirectoryPath);
                        if (modsDirectoryInfo.Exists)
                        {
                            FileInfo[] modFileInfos = modsDirectoryInfo.GetFiles("*.zip", SearchOption.TopDirectoryOnly);
                            if (modFileInfos.Length != 0) if (UiFunctions.ShowQuestion("Would you like to auto-generate a modpack from your default mods folder?")) AutogenerateModPack(modFileInfos);
                        }

                        gameSettings.IsAutogeneratedModPackExecuted = true;
                    }
                    catch (Exception ex)
                    {
                        UiFunctions.ShowError(ex);
                    }
                });
            });
        }

        private void AutogenerateModPack(FileInfo[] modFileInfos)
        {
            GameClientBase client;
            switch (_game)
            {
                case FSimMan.Management.Game.FarmingSim22:
                    client = new Fs22Client(false);
                    break;
                case FSimMan.Management.Game.FarmingSim25:
                    client = new Fs25Client(false);
                    break;
                default:
                    throw new NotImplementedException();
            }

            client.AutoGenerateModPack(modFileInfos);
        }
        #endregion
    }
}
