using OF.Base.Client;
using OF.FSimMan.Client.ImportExport.Fsmmp;
using OF.FSimMan.Client.Management;
using OF.FSimMan.Game;
using OF.FSimMan.Management;
using OF.FSimMan.Utility;
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
                        return Path.Combine(SettingsClient.Instance.AppSettings.Fs22DataPath, FILE_NAME_GAMESETTINGS);
                    case FSimMan.Management.Game.FarmingSim25:
                        return Path.Combine(SettingsClient.Instance.AppSettings.Fs25DataPath, FILE_NAME_GAMESETTINGS);
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

        private string _processName
        {
            get
            {
                switch (Game)
                {
                    case FSimMan.Management.Game.FarmingSim22:
                        return "FarmingSimulator2022Game";
                    case FSimMan.Management.Game.FarmingSim25:
                        return "FarmingSimulator2025Game";
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        private bool _isGameRunning = false;
        public bool IsGameRunning
        {
            get
            {
                bool stateBefore = _isGameRunning;
                if (SetProperty(ref _isGameRunning, Process.GetProcessesByName(_processName).Length > 0))
                {
                    InvokeGameStateChanged();
                    if (stateBefore == true && _isGameRunning == false && GameState == GameState.Started) GameState = GameState.SelfStopped;
                }
                return _isGameRunning;
            }
        }

        private GameState _gameState = GameState.Stopped;
        public GameState GameState
        {
            get => _gameState;
            private set { if (SetProperty(ref _gameState, value)) InvokeGameStateChanged(); }
        }
        #endregion

        #region Events
        public event EventHandler? GameStateChanged;
        #endregion

        #region Constructor & Initialize
        public GameClientBase(FSimMan.Management.Game game) : base()
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

                _modPacksEditor?.CancelEdit();

                ModPacksData data = FileSerializationHelper.DeserializeConfigFile<ModPacksData>(_modPacksFileName);
                ModPacks = data.FromData();

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

                GameState = GameState.Started;
                SetGameModFolder();
                ExecuteGameExe();
                WaitForGameState(GameState.Started, true);
            }
            finally
            {
                ResetBusyIndicator();
            }
        }

        public void StopGame()
        {
            try
            {
                IsBusy = true;

                GameState = GameState.Stopped;
                KillGameProcess();
                WaitForGameState(GameState.Stopped, false);
            }
            finally
            {
                ResetBusyIndicator();
            }
        }

        public void WaitForGameState(GameState gameState, bool isGameRunning)
        {
            while (GameState != gameState | IsGameRunning != isGameRunning)
            {
                Thread.Sleep(1000);
            }
        }

        public void ExportModPack(ModPack modPack, string filePath)
        {
            FsmmpImportExportClient client = new FsmmpImportExportClient();
            client.Export(filePath, modPack);
        }
        #endregion

        #region Methods INTERNAL
        internal void StoreModPacks()
        {
            ModPacksData data = new ModPacksData();
            data.ToData(ModPacks);

            FileSerializationHelper.SerializeConfigFile(_modPacksFileName, data);
        }
        #endregion

        #region Methods PROTECTED
        protected abstract void ReadGameSettings();
        protected abstract void SetGameModFolder();
        protected abstract void StoreGameSettings();
        #endregion

        #region Methods PRIVATE
        private void InvokeGameStateChanged()
        {
            GameStateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ExecuteGameExe()
        {
            string exePath;
            switch (Game)
            {
                case FSimMan.Management.Game.FarmingSim22:
                    exePath = Path.Combine(SettingsClient.Instance.AppSettings.Fs22GamePath, "FarmingSimulator2022.exe");
                    break;
                case FSimMan.Management.Game.FarmingSim25:
                    exePath = Path.Combine(SettingsClient.Instance.AppSettings.Fs22GamePath, "FarmingSimulator2025.exe");
                    break;
                default:
                    throw new NotImplementedException();
            }

            Process.Start(exePath);
        }

        private void KillGameProcess()
        {
            Process[] processes = Process.GetProcessesByName(_processName);
            processes[0].Kill(true);
        }
        #endregion
    }
}
