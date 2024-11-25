using OF.Base.Client;
using OF.FSimMan.Management;

namespace OF.FSimMan.Client.Base
{
    public abstract class GameClientBase : ClientBase
    {
        private Game _game;

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
        public GameClientBase(Game game)
        {
            _game = game;
        }
        #endregion
    }
}
