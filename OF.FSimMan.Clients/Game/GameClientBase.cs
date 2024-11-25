using OF.Base.Client;

namespace OF.FSimMan.Client.Game
{
    public abstract class GameClientBase : ClientBase
    {
        private FSimMan.Management.Game _game;

        #region Properties
        private string _installDirectoryPath = string.Empty;
        internal string InstallDirectoryPath
        {
            get => _installDirectoryPath;
        }

        private string _userDataPath = string.Empty;
        internal string UserDataPath
        {
            get => _userDataPath;
        }
        #endregion

        #region Constructor
        public GameClientBase(FSimMan.Management.Game game)
        {
            _game = game;
        }
        #endregion
    }
}
