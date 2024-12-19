namespace OF.FSimMan.Management
{
    public class AppSettingsModPackAutogenerationNowPossibleEventArgs : EventArgs
    {
        #region Properties
        private readonly Game _game;
        public Game Game => _game;
        #endregion

        #region Constructor
        public AppSettingsModPackAutogenerationNowPossibleEventArgs(Game game) {
            _game = game;
        }
        #endregion
    }
}
