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

        private List<PossibleDlc>? _possibleDlcs = null;
        public List<PossibleDlc> PossibleDlcs
        {
            get
            {
                if (_possibleDlcs is null) _possibleDlcs = KnownDlc.List.Select(k => new PossibleDlc(k)).ToList();
                return _possibleDlcs;
            }
        }
        #endregion

        #region Constructor
        public EditModPackClientBase(ModPack modPack, GameClientBase gameClient)
        {
            _modPack = modPack;
            _gameClient = gameClient;
            _modPackEditor = new ModPackEditor(_modPack);
            RefreshPossibleDlcs();
        }
        #endregion

        #region Methods PUBLIC
        public void StoreModPack()
        {
            _modPackEditor.SetDlcRequirements(PossibleDlcs);
            _modPackEditor.TriggerEndEdit();
            _modPack = _gameClient.GetDbAccess().StoreModPack(_modPack);
            _modPackEditor = new ModPackEditor(_modPack);
            OnPropertyChanged(nameof(ModPack));
            RefreshPossibleDlcs();
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

        public void AddIcon(string filePath)
        {
            _modPackEditor.AddIcon(filePath);
        }

        public void RemoveIcon()
        {
            _modPackEditor.RemoveIcon();
        }
        #endregion

        #region Methods PRIVATE
        private void RefreshPossibleDlcs() // TODOI: Move to ModPackEditor?
        {
            List<KnownDlc> knownDlcs = KnownDlc.GetByGame(_gameClient.Game);
            List<PossibleDlc> possibleDlcs = knownDlcs.Select(k => new PossibleDlc(k)).ToList();

            foreach (DlcRequirement dlcRequirement in _modPack.Dlcs)
            {
                PossibleDlc? possibleDlc = possibleDlcs.Where(d => d.Dlc.FileName.Equals(dlcRequirement.Dlc?.FileName)).SingleOrDefault();
                if (possibleDlc is not null) possibleDlc.IsRequired = true;
            }

            _possibleDlcs = possibleDlcs;
            OnPropertyChanged(nameof(PossibleDlcs));
        }
        #endregion
    }
}
