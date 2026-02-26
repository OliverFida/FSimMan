using OF.FSimMan.Game.Exceptions;
using OF.FSimMan.Game.Fs.Fs22;
using OF.FSimMan.Utility;

namespace OF.FSimMan.Client.Game.Fs
{
    public class Fs22Client : GameClientBase
    {
        #region Constructor
        public Fs22Client() : base(FSimMan.Management.Game.FarmingSim22) { }
        public Fs22Client(bool doInitialize) : base(FSimMan.Management.Game.FarmingSim22, doInitialize) { }
        #endregion

        #region Methods INTERNAL
        internal override void ResetGameModFolder()
        {
            if (_gameSettings is null) return;

            gameSettingsModsDirectoryOverride directoryOverride = new()
            {
                active = false,
                directory = string.Empty
            };

            ((gameSettings)_gameSettings).modsDirectoryOverride = directoryOverride;
            StoreGameSettings();
        }
        #endregion

        #region Methods PROTECTED
        protected override void ReadGameSettings()
        {
            if (!File.Exists(GameSettingsFilePath))
            {
                SetProperty(ref _gameSettings, null);
                return;
            }

            gameSettings gameSettings = FileSerializationHelper.DeserializeAnyFile<gameSettings>(GameSettingsFilePath);
            if (gameSettings.volume is null) throw new InvalidGameSettingsFileException();

            SetProperty(ref _gameSettings, gameSettings);
        }

        protected override void SetGameModFolder()
        {
            if (SelectedModPack is null)
            {
                ResetGameModFolder();
                return;
            }

            if (_gameSettings is null) return;

            gameSettingsModsDirectoryOverride directoryOverride = new()
            {
                active = true,
                directory = SelectedModPack.ModsDirectoryPath
            };

            ((gameSettings)_gameSettings).modsDirectoryOverride = directoryOverride;
            StoreGameSettings();
        }

        protected override void StoreGameSettings()
        {
            if (_gameSettings is null) return;

            FileSerializationHelper.SerializeAnyFile(GameSettingsFilePath, (gameSettings)_gameSettings);
        }
        #endregion
    }
}
