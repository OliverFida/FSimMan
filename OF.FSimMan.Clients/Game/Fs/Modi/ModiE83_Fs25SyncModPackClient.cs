using OF.Base.Client;
using OF.Base.Objects;
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
            CheckSettings();

            _modPackEditor.TriggerEndEdit();
            _modPack = _gameClient.GetDbAccess().StoreModPack(_modPack);
            _modPackEditor = new ModPackEditor(_modPack);
        }

        public void CancelEdit()
        {
            _modPackEditor.TriggerCancelEdit();
        }
        #endregion

        #region Methods PRIVATE
        private void CheckSettings()
        {
            // If not active => ignore rest
            if (!ModPack.ModiE83_IsSyncEnabled) return;

            if (!Directory.Exists(ModPack.ModiE83_SyncPath))
                throw new OfException("Sync Path does not exist!");
        }
        //string expectedFileName = _modPack.GetExportFileName();
        #endregion
    }
}
