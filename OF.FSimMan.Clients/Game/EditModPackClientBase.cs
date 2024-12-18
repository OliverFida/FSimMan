using OF.Base.Client;
using OF.FSimMan.Game;

namespace OF.FSimMan.Client.Game
{
    public abstract class EditModPackClientBase : ClientBase
    {
        #region Properties
        private readonly ModPack _modPack;
        public ModPack ModPack => _modPack;

        protected readonly GameClientBase _gameClient;

        private readonly ModPackEditor _modPackEditor;
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
            _gameClient.StoreModPacks();
            _modPackEditor.TriggerBeginEdit();
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
