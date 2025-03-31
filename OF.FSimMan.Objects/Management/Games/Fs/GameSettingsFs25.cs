using OF.FSimMan.Management.Exceptions;

namespace OF.FSimMan.Management.Games.Fs
{
    public class GameSettingsFs25 : GameSettingsBase
    {
        #region Properties
        public override string ExeFileName { get => "FarmingSimulator2025.exe"; }
        public override string SteamId { get => "2300320"; }
        #endregion

        #region Constructor
        public GameSettingsFs25() : base(Game.FarmingSim25) { }
        #endregion

        #region Methods PUBLIC
        public override void ValidateExeDirectoryPath(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (file.EndsWith("FarmingSimulator2025.exe")) return;
            }
            throw new GamePathIncorrectException();
        }

        public override void ValidateDataDirectoryPath(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (file.EndsWith("gameSettings.xml")) return;
            }
            throw new DataPathIncorrectException();
        }
        #endregion
    }
}
