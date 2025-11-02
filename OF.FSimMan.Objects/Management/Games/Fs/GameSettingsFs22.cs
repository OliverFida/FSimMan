namespace OF.FSimMan.Management.Games.Fs
{
    public class GameSettingsFs22 : GameSettingsBase
    {
        #region Properties
        public override string ExeFileName { get => "FarmingSimulator2022.exe"; }
        public override string SettingsFileName { get => "gameSettings.xml"; }
        public override string SteamId { get => "1248130"; }
        #endregion

        #region Constructor
        public GameSettingsFs22() : base(Game.FarmingSim22) { }
        #endregion
    }
}
