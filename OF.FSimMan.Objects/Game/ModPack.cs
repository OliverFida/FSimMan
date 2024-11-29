﻿using OF.Base.Objects;

namespace OF.FSimMan.Game
{
    public class ModPack : EditableBindingObject
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

        internal string? _description;
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

        internal EditableObservableCollection<Mod> _mods = new EditableObservableCollection<Mod>();
        public EditableObservableCollection<Mod> Mods
        {
            get => _mods;
            private set => SetProperty(ref _mods, value);
        }

        private string _modPackDirectoryPath
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
                string temp = Path.Combine(_modPackDirectoryPath, "mods");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }

        private string _modsTempDirectoryPath
        {
            get
            {
                string temp = Path.Combine(_modPackDirectoryPath, "modsTemp");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }

        public string ModIconsDirectoryPath
        {
            get
            {
                string temp = Path.Combine(_modPackDirectoryPath, "modIcons");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }
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
