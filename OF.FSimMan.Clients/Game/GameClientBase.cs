using OF.Base.Client;
using OF.FSimMan.Client.ImportExport.Fsmmp;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Database.Access;
using OF.FSimMan.Game;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games;
using OF.FSimMan.Management.Games.Fs;
using System.Diagnostics;

namespace OF.FSimMan.Client.Game
{
    public abstract class GameClientBase : ClientBase, IGameClient
    {

        #region Properties
        private const string FILE_NAME_GAMESETTINGS = "gameSettings.xml";

        protected string GameSettingsFilePath
        {
            get
            {
                switch (_game)
                {
                    case FSimMan.Management.Game.FarmingSim22:
                        return Path.Combine(SettingsClient.Instance.AppSettings.GetGameSettings<GameSettingsFs22>().DataDirectoryPath, FILE_NAME_GAMESETTINGS);
                    case FSimMan.Management.Game.FarmingSim25:
                        return Path.Combine(SettingsClient.Instance.AppSettings.GetGameSettings<GameSettingsFs25>().DataDirectoryPath, FILE_NAME_GAMESETTINGS);
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        protected IGameSettings? _gameSettings;

        private readonly FSimMan.Management.Game _game;
        public FSimMan.Management.Game Game => _game;

        private ModPacks _modPacks = new ModPacks();
        public ModPacks ModPacks
        {
            get => _modPacks;
            private set => SetProperty(ref _modPacks, value);
        }

        private string _modPacksFileName
        {
            get => $"modPacks{_game.ToString()}.xml";
        }

        private ModPacksEditor? _modPacksEditor;

        private ModPack? _selectedModPack;
        public ModPack? SelectedModPack
        {
            get => _selectedModPack;
            set => SetProperty(ref _selectedModPack, value);
        }
        #endregion

        #region Events
        public event EventHandler? GameStateChanged;
        #endregion

        #region Constructor & Initialize
        public GameClientBase(FSimMan.Management.Game game, bool doInitialize = true) : base()
        {
            _game = game;
        }

        public override async Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                ReadGameSettings();
                RefreshModPacks(false);
            }
            finally
            {
                ResetBusyIndicator();
            }

            await base.InitializeAsync();
        }
        #endregion

        #region Methods PUBLIC
        public void RefreshModPacks(bool doControlBusyIndicator = true)
        {
            try
            {
                if (doControlBusyIndicator) IsBusy = true;

                _modPacksEditor?.TriggerCancelEdit();

                ModPacks = GetDbAccess().ReadModPacks();

                _modPacksEditor = new ModPacksEditor(ModPacks);
            }
            finally
            {
                if (doControlBusyIndicator) ResetBusyIndicator();
            }
        }

        public ModPack GetNewModPack()
        {
            try
            {
                IsBusy = true;

                ModPack newModPack = new ModPack(_game);
                _modPacksEditor!.AddModPack(newModPack);
                return newModPack;
            }
            finally
            {
                ResetBusyIndicator();
            }
        }

        public void DeleteModPack(ModPack modPack)
        {
            try
            {
                IsBusy = true;

                _modPacksEditor!.RemoveModPack(modPack);
                Directory.Delete(modPack.ModPackDirectoryPath, true);
                StoreModPacks();
                RefreshModPacks(false);
            }
            finally
            {
                ResetBusyIndicator();
            }
        }

        public void RunGame()
        {
            try
            {
                IsBusy = true;

                SetGameModFolder();
                ExecuteGame();
            }
            finally
            {
                ResetBusyIndicator();
            }
        }

        //public void StopGame()
        //{
        //    try
        //    {
        //        IsBusy = true;

        //        KillGameProcess();
        //    }
        //    finally
        //    {
        //        ResetBusyIndicator();
        //    }
        //}

        public void ExportModPack(ModPack modPack, string filePath)
        {
            FsmmpImportExportClient client = new FsmmpImportExportClient(Game);
            client.Export(filePath, modPack);
        }

        public bool GetModPackExists(string filePath)
        {
            Guid importGuid = FsmmpImportExportClient.GetModPackGuid(filePath);
            ModPack? matchingModPack = (from p in ModPacks.List where p.Guid.ToString().Equals(importGuid.ToString()) select p).SingleOrDefault();

            return matchingModPack is not null;
        }

        public void ImportModPack(string filePath, bool importAsNew)
        {
            FsmmpImportExportClient client = new FsmmpImportExportClient(Game);
            ModPack importedModPack = client.Import(filePath, importAsNew);
            _modPacksEditor!.AddModPack(importedModPack, !importAsNew);
            StoreModPacks();
            RefreshModPacks();
        }

        public void AutoGenerateModPack(FileInfo[] modFileInfos)
        {
            try
            {
                IsBusy = true;

                _modPacksEditor = new ModPacksEditor(ModPacks);
                ModPack newModPack = new ModPack(_game)
                {
                    Title = "Default",
                    Description = "Auto-imported modpack",
                    Author = "FSimMan",
                    Version = "1.0"
                };

                string[] modFilePaths = modFileInfos.Select(x => x.FullName).ToArray();

                ModPackEditor modPackEditor = new ModPackEditor(newModPack);
                modPackEditor.AddMods(modFilePaths);
                modPackEditor.TriggerEndEdit();

                _modPacksEditor.AddModPack(newModPack);
                StoreModPacks();

                Parallel.ForEach(modFileInfos, modFileInfo =>
                {
                    modFileInfo.Delete();
                });
            }
            finally
            {
                ResetBusyIndicator();
            }
        }
        #endregion

        #region Methods INTERNAL
        internal void StoreModPacks()
        {
            _modPacksEditor?.TriggerCancelEdit();

            ModPacks = GetDbAccess().BulkStoreModPacks(ModPacks);

            _modPacksEditor = new ModPacksEditor(ModPacks);
        }

        internal IModPacksDbAccess GetDbAccess()
        {
            switch (_game)
            {
                case FSimMan.Management.Game.FarmingSim22:
                    return ModPacksFs22DbAccess.Instance;
                case FSimMan.Management.Game.FarmingSim25:
                    return ModPacksFs25DbAccess.Instance;
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion

        #region Methods PROTECTED
        protected abstract void ReadGameSettings();
        protected abstract void SetGameModFolder();
        protected abstract void StoreGameSettings();
        #endregion

        #region Methods PRIVATE
        private void ExecuteGame()
        {
            GameSettingsBase gameSettings;
            switch (_game)
            {
                case FSimMan.Management.Game.FarmingSim22:
                    gameSettings = SettingsClient.Instance.AppSettings.GetGameSettings<GameSettingsFs22>();
                    break;
                case FSimMan.Management.Game.FarmingSim25:
                    gameSettings = SettingsClient.Instance.AppSettings.GetGameSettings<GameSettingsFs25>();
                    break;
                default:
                    throw new NotImplementedException();

            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            switch (gameSettings.GameOrigin)
            {
                case FSimMan.Game.GameOrigin.DvdWebsite:
                    processStartInfo.FileName = gameSettings.ExeFilePath;
                    processStartInfo.Arguments = gameSettings.StartArguments.GetArgumentsString();
                    break;
                case FSimMan.Game.GameOrigin.Steam:
                    processStartInfo.FileName = GetGameSteamUri(gameSettings);
                    processStartInfo.UseShellExecute = true;
                    break;
                default:
                    throw new NotImplementedException();
            }

            Process.Start(processStartInfo);
        }

        private string GetGameSteamUri(GameSettingsBase gameSettings)
        {
            List<string> uriParts = new List<string>
            {
                "steam://rungameid",
                gameSettings.SteamId
            };

            string temp = string.Join("/", uriParts);
            List<string> arguments = gameSettings.StartArguments.GetArgumentsList();
            if (arguments.Count.Equals(0)) return temp;

            uriParts.Clear();
            temp += "/";
            uriParts.Add(temp);
            uriParts.AddRange(arguments);

            return string.Join("/", uriParts);
        }

        //private void KillGameProcess()
        //{
        //    Process[] processes = Process.GetProcessesByName(_processName);
        //    if (processes.Length == 0) return;

        //    processes[0].Kill(true);
        //}
        #endregion
    }
}
