using OF.Base.Objects;

namespace OF.FSimMan.Game
{
    public class ModPack : EditableBindingObject
    {
        #region Properties
        internal Guid _guid = Guid.NewGuid();
        public Guid Guid
        {
            get => _guid;
        }

        internal FSimMan.Management.Game _game;

        internal string _title = "New modpack";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        internal string? _version;
        public string? Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }
        public bool IsVersionVisible
        {
            get => !string.IsNullOrWhiteSpace(Version);
        }

        internal string? _author;
        public string? Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        internal string? _description = string.Empty;
        public string? Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        internal string? _imageSource;
        public string? ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        //internal EditableObservableCollection<Mod> _mods = new EditableObservableCollection<Mod>();
        //public EditableObservableCollection<Mod> Mods
        //{
        //    get => _mods;
        //    private set => SetProperty(ref _mods, value);
        //}
        #endregion

        #region Constructor
        internal ModPack() { }

        public ModPack(Management.Game game)
        {
            _game = game;
        }
        #endregion
    }
}
