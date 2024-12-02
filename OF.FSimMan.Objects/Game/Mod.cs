using OF.Base.Objects;

namespace OF.FSimMan.Game
{
    public class Mod : EditableBindingObject
    {
        internal ModPack _parent;

        #region Properties
        internal string _fileName;
        public string FileName
        {
            get => _fileName;
        }

        internal string _title = "Unnamed mod";
        public string Title
        {
            get => _title;
        }

        internal string? _version;
        public string? Version
        {
            get => _version;
        }

        internal string? _author;
        public string? Author
        {
            get => _author;
        }

        internal string? _description;
        public string? Description
        {
            get => _description;
        }

        internal bool _isMultiplayerCompatible = false;
        public bool IsMultiplayerCompatible
        {
            get => _isMultiplayerCompatible;
        }

        internal string? _imageSource;
        public string? ImageSource
        {
            get => _imageSource;
        }
        public string? FullImageSource
        {
            get => !string.IsNullOrWhiteSpace(_imageSource) ? Path.Combine(_parent?.ModIconsDirectoryPath ?? "", _imageSource!) : null;
        }
        #endregion

        #region Constructor
        public Mod(ModPack parent, string fileName)
        {
            _parent = parent;
            _fileName = fileName;
        }
        #endregion
    }
}
