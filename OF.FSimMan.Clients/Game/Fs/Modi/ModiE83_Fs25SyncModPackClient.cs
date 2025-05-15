using OF.Base.Client;
using OF.FSimMan.Game;

namespace OF.FSimMan.Client.Game.Fs.Modi
{
    public class ModiE83_Fs25SyncModPackClient : ClientBase
    {
        #region Properties
        private ModPack _modPack;
        public ModPack ModPack
        {
            get => _modPack;
        }

        protected readonly Fs25Client _gameClient;

        private ModPackEditor _modPackEditor;
        #endregion

        #region Constructor
        public ModiE83_Fs25SyncModPackClient(ModPack modPack, Fs25Client gameClient)
        {
            _modPack = modPack;
            _gameClient = gameClient;
            _modPackEditor = new ModPackEditor(_modPack);
        }
        #endregion

        #region Methods PUBLIC
        public void Store()
        {
            _modPackEditor.TriggerEndEdit();
            _modPack = _gameClient.GetDbAccess().StoreModPack(_modPack);
            _modPackEditor = new ModPackEditor(_modPack);
        }

        public void CancelEdit()
        {
            _modPackEditor.TriggerCancelEdit();
        }
        #endregion
    }
}
