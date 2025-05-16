using OF.Base.Objects;

namespace OF.FSimMan.Game
{
    public class ModPack : EditableObject
    {
        #region Properties
        internal Management.Game _game;

        internal Guid _guid = Guid.NewGuid();
        public Guid Guid
        {
            get => _guid;
        }

        internal string _title = "New modpack";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        internal string _version = string.Empty;
        public string Version
        {
            get => _version;
            set => SetProperty(ref _version, value);
        }
        public bool IsVersionVisible
        {
            get => !string.IsNullOrWhiteSpace(Version);
        }

        internal string _author = string.Empty;
        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        internal string _description = string.Empty;
        public string Description
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

        internal EditableObservableCollection<Mod> _mods = new EditableObservableCollection<Mod>();
        public EditableObservableCollection<Mod> Mods
        {
            get => _mods;
            private set => SetProperty(ref _mods, value);
        }

        public string ModPackDirectoryPath
        {
            get
            {
                string temp = Path.Combine(CurrentApplication.MODPACKS_PATH, _game.ToString(), Guid.ToString());
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }

        public string ModsDirectoryPath
        {
            get
            {
                string temp = Path.Combine(ModPackDirectoryPath, "mods");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }

        public string ModsTempDirectoryPath
        {
            get
            {
                string temp = Path.Combine(ModPackDirectoryPath, "modsTemp");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }

        public string ModIconsDirectoryPath
        {
            get
            {
                string temp = Path.Combine(ModPackDirectoryPath, "modIcons");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }

        // MODI E83
        internal string _modiE83_SyncPath = string.Empty;
        public string ModiE83_SyncPath
        {
            get => _modiE83_SyncPath;
            set => SetProperty(ref _modiE83_SyncPath, value);
        }

        internal bool _modiE83_IsSyncEnabled = false;
        public bool ModiE83_IsSyncEnabled
        {
            get => _modiE83_IsSyncEnabled;
            set => SetProperty(ref _modiE83_IsSyncEnabled, value);
        }
        #endregion

        #region Constructor
        internal ModPack() { }

        public ModPack(Management.Game game)
        {
            _game = game;
        }
        #endregion

        #region Methods PUBLIC
        public string GetExportFileName()
        {
            List<string> fileNameParts = new List<string>()
            {
                _game.ToString().ToLower(), // Game Name
                Title.Replace(" ", "")// Title
            };
            if (!string.IsNullOrWhiteSpace(Version)) fileNameParts.Add($"v{Version}"); // Version

            return String.Join("_", fileNameParts) + ".fsmmp";
        }
        #endregion
    }
}
