using OF.FSimMan.Game.Exceptions;
using OF.FSimMan.Game.Fs.Fs25;
using OF.FSimMan.Utility;

namespace OF.FSimMan.Client.Game.Fs
{
    public class Fs25Client : GameClientBase
    {
        #region Constructor
        public Fs25Client() : base(FSimMan.Management.Game.FarmingSim25) { }
        public Fs25Client(bool doInitialize) : base(FSimMan.Management.Game.FarmingSim25, doInitialize) { }
        #endregion

        #region Methods PROTECTED
        protected override void ReadGameSettings()
        {
            if (!File.Exists(GameSettingsFilePath))
            {
                SetProperty(ref _gameSettings, null);
                return;
            }

            gameSettings gameSettings = FileSerializationHelper.DeserializeFile<gameSettings>(GameSettingsFilePath);
            if (gameSettings.volume is null) throw new InvalidGameSettingsFileException();

            SetProperty(ref _gameSettings, gameSettings);
        }

        protected override void SetGameModFolder()
        {
            if (_gameSettings is null) return;

            gameSettingsModsDirectoryOverride directoryOverride = new gameSettingsModsDirectoryOverride();
            if (SelectedModPack is null)
            {
                directoryOverride.active = false;
                directoryOverride.directory = string.Empty;
            }
            else
            {
                directoryOverride.active = true;
                directoryOverride.directory = SelectedModPack.ModsDirectoryPath;
            }

            ((gameSettings)_gameSettings).modsDirectoryOverride = directoryOverride;
            StoreGameSettings();
        }

        protected override void StoreGameSettings()
        {
            if (_gameSettings is null) return;

            FileSerializationHelper.SerializeFile(GameSettingsFilePath, (gameSettings)_gameSettings);
        }
        #endregion
    }
}
