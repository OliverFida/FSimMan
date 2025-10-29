namespace OF.FSimMan.Management.Games.Fs
{
    public class GameSettingsFs25 : GameSettingsBase
    {
        #region Properties
        public override string ExeFileName { get => "FarmingSimulator2025.exe"; }
        public override string SettingsFileName { get => "gameSettings.xml"; }
        public override string SteamId { get => "2300320"; }
        #endregion

        #region Constructor
        public GameSettingsFs25() : base(Game.FarmingSim25) { }
        #endregion
    }
}
