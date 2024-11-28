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

        private ModPackEditor _modPackEditor;
        #endregion

        #region Constructor
        public EditModPackClientBase(ModPack modPack)
        {
            _modPack = modPack;
            _modPackEditor = new ModPackEditor(_modPack);
        }
        #endregion
    }
}
