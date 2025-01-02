using OF.Base.Client;
using OF.FSimMan.Game;

namespace OF.FSimMan.Client.Game
{
    public abstract class EditModPackClientBase : ClientBase
    {
        #region Properties
        private ModPack _modPack;
        public ModPack ModPack
        {
            get => _modPack;
        }

        protected readonly GameClientBase _gameClient;

        private ModPackEditor _modPackEditor;
        #endregion

        #region Constructor
        public EditModPackClientBase(ModPack modPack, GameClientBase gameClient)
        {
            _modPack = modPack;
            _gameClient = gameClient;
            _modPackEditor = new ModPackEditor(_modPack);
        }
        #endregion

        #region Methods PUBLIC
        public void StoreModPack()
        {
            _modPackEditor.TriggerEndEdit();
            _modPack = _gameClient.GetDbAccess().StoreModPack(_modPack);
            _modPackEditor = new ModPackEditor(_modPack);
        }

        public void CancelEdit()
        {
            _modPackEditor.TriggerCancelEdit();
        }

        public void AddMods(string[] filePaths)
        {
            _modPackEditor.AddMods(filePaths);
        }

        public void RemoveMod(Mod mod)
        {
            _modPackEditor.RemoveMod(mod);
        }
        #endregion
    }
}
