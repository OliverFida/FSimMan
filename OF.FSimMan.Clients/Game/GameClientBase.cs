using OF.Base.Client;
using OF.FSimMan.Client.Utility;
using OF.FSimMan.Game;

namespace OF.FSimMan.Client.Game
{
    public abstract class GameClientBase : ClientBase, IGameClient
    {

        #region Properties
        private readonly FSimMan.Management.Game _game;
        public FSimMan.Management.Game Game => _game;

        private ModPacks _modPacks = new ModPacks();
        public ModPacks ModPacks
        {
            get => _modPacks;
            private set => SetProperty(ref _modPacks, value);
        }

        private string _modPacksFileName
        {
            get
            {
                switch (_game)
                {
                    case FSimMan.Management.Game.FarmingSim22:
                        return "modPacksFs22.xml";
                    case FSimMan.Management.Game.FarmingSim25:
                        return "modPacksFs25.xml";
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private ModPacksEditor? _modPacksEditor;
        #endregion

        #region Constructor & Initialize
        public GameClientBase(FSimMan.Management.Game game)
        {
            _game = game;
        }

        public override Task InitializeAsync()
        {
            try
            {
                IsBusy = true;

                RefreshModPacks(false);
            }
            finally
            {
                ResetBusyIndicator();
            }

            return Task.CompletedTask;
        }
        #endregion

        #region Methods PUBLIC
        public void RefreshModPacks(bool doControlBusyIndicator = true)
        {
            try
            {
                if (doControlBusyIndicator) IsBusy = true;

                _modPacksEditor?.CancelEdit();

                ModPacksData data = FileSerializationHelper.DeserializeConfigFile<ModPacksData>(_modPacksFileName);
                ModPacks = data.FromData();

                _modPacksEditor = new ModPacksEditor(ModPacks);
            }
            finally
            {
                if (doControlBusyIndicator) ResetBusyIndicator();
            }
        }

        public void CreateNewModPack()
        {
            // OFDO
        }

        public void DeleteModPack(ModPack modPack)
        {
            try
            {
                IsBusy = true;

                _modPacksEditor!.RemoveModPack(modPack);
                StoreModPacks();
                RefreshModPacks(false);
            }
            finally
            {
                ResetBusyIndicator();
            }
        }
        #endregion

        #region Methods PRIVATE
        private void StoreModPacks()
        {
            ModPacksData data = new ModPacksData();
            data.ToData(ModPacks);

            FileSerializationHelper.SerializeConfigFile(_modPacksFileName, data);
        }
        #endregion
    }
}
