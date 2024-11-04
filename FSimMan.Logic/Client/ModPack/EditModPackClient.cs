using OliverFida.Base;

namespace OliverFida.FSimMan.Client.ModPack
{
    public class EditModPackClient : ClientBase
    {
        private FsBaseClient _fsBaseClient;

        private EditMode _editMode;
        public EditMode EditMode { get => _editMode; }

        private Config.ModPack.ModPack _modPack;
        public Config.ModPack.ModPack ModPack
        {
            get => _modPack;
        }

        public EditModPackClient(FsBaseClient fsBaseClient, Config.ModPack.ModPack modPack, EditMode editMode)
        {
            _fsBaseClient = fsBaseClient;
            _editMode = editMode;
            _modPack = modPack;

            ModPack.BeginEdit();
        }

        public void CancelEditing()
        {
            ModPack.CancelEdit();
            ModPack.CheckModFiles(true);
            if (_editMode == EditMode.New) _fsBaseClient.ConfigModPacks!.CancelEdit();
        }

        public void StoreModPack()
        {
            ModPack.EndEdit();
            ModPack.CheckModFiles(true);
            _fsBaseClient.StoreModPacks();
            _editMode = EditMode.Edit;
            ModPack.BeginEdit();
        }
    }
}
