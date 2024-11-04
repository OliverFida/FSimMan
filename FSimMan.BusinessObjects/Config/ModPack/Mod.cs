using OliverFida.Base;

namespace OliverFida.FSimMan.Config.ModPack
{
    public class Mod : EditableObjectBase
    {
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

        internal string _imageSource = string.Empty;
        public string ImageSource
        {
            get => _imageSource;
        }

        internal string _fileName;
        public string FileName
        {
            get => _fileName;
        }

        public Mod(string title, string fileName)
        {
            _title = title;
            _fileName = fileName;
        }
    }
}
