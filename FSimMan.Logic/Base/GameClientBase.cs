using OF.Base.Client;

namespace OliverFida.FSimMan.Base
{
    public abstract class GameClientBase : ClientBase
    {
        private string _gamePath = string.Empty;
        internal string GamePath
        {
            get => _gamePath;
        }

        private string _dataPath = string.Empty;
        internal string DataPath
        {
            get => _dataPath;
        }
    }
}
