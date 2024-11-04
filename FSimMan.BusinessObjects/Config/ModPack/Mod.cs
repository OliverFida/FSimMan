using OliverFida.Base;

namespace OliverFida.FSimMan.Config.ModPack
{
    public class Mod : EditableObjectBase
    {
        internal ModPack _parent;

        internal string _title = "New mod";
        public string Title
        {
            get => _title;
        }

        internal string _version = string.Empty;
        public string Version
        {
            get => _version;
        }

        internal string _author = "unknown";
        public string Author
        {
            get => _author;
        }

        internal string _description = string.Empty;
        public string Description
        {
            get => _description;
        }

        internal bool _isMultiplayerCompatible = false;
        public bool IsMultiplayerCompatible
        {
            get => _isMultiplayerCompatible;
        }

        internal string? _imageSource = null;
        public string? ImageSource
        {
            get => _imageSource;
        }
        public string? FullImageSource
        {
            get => !string.IsNullOrWhiteSpace(_imageSource) ? Path.Combine(_parent.ModIconsDirectoryPath, _imageSource!) : null;
        }

        internal string _fileName;
        public string FileName
        {
            get => _fileName;
        }

        public Mod(ModPack parent, string title, string fileName)
        {
            _parent = parent;
            _title = title;
            _fileName = fileName;
        }
    }
}
