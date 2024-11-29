using OF.FSimMan.Game.Exceptions;
using OF.FSimMan.Game.Fs.Fs22;
using OF.FSimMan.Utility;

namespace OF.FSimMan.Client.Game.Fs
{
    public class Fs22Client : FsClientBase
    {
        #region Constructor
        public Fs22Client() : base(FSimMan.Management.Game.FarmingSim22) { }
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

            FileSerializationHelper.SerializeFile<gameSettings>(GameSettingsFilePath, (gameSettings)_gameSettings);
        }

        #endregion
    }
}
