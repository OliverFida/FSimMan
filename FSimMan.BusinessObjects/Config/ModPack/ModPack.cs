using OliverFida.Base;
using OliverFida.FSimMan.Exceptions;
using OliverFida.FSimMan.Exceptions.Fs;
using OliverFida.FSimMan.FS22.Mod;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;

namespace OliverFida.FSimMan.Config.ModPack
{
    public class ModPack : EditableObjectBase
    {
        #region Properties
        internal FsEdition _fsEdition;

        internal string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        internal string _author;
        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
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

        internal string? _imageSource;
        public string? ImageSource
        {
            get => _imageSource;
            set => SetProperty(ref _imageSource, value);
        }

        internal Guid _key = Guid.NewGuid();
        public Guid Key
        {
            get => _key;
        }

        internal string _description = string.Empty;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        internal EditableObservableCollection<Mod> _mods = new EditableObservableCollection<Mod>();
        public EditableObservableCollection<Mod> Mods
        {
            get => _mods;
            private set => SetProperty(ref _mods, value);
        }

        private string _modPackDirectoryPath
        {
            get => Path.Combine(CurrentApplication.MODPACKS_PATH, _fsEdition.ToString(), Key.ToString());
        }

        public string ModsDirectoryPath
        {
            get => Path.Combine(_modPackDirectoryPath, "mods");
        }

        private string _modsTempDirectoryPath
        {
            get => Path.Combine(_modPackDirectoryPath, "modsTemp");
        }

        public string ModIconsDirectoryPath
        {
            get => Path.Combine(_modPackDirectoryPath, "modIcons");
        }
        #endregion

        #region Constructor
        public ModPack(string title, string author, FsEdition fsEdition)
        {
            _fsEdition = fsEdition;
            _title = title;
            _author = author;
        }
        #endregion

        #region Methods PUBLIC
        public void AddMods(string[] filePaths)
        {
            foreach (string filePath in filePaths)
            {
                AddMod(filePath);
            }
            CheckModFiles();
        }

        public void CheckModFiles() => CheckModFiles(false);
        public void CheckModFiles(bool final)
        {
            if (!Directory.Exists(ModsDirectoryPath)) Directory.CreateDirectory(ModsDirectoryPath);
            string[] modFilePaths = Directory.GetFiles(ModsDirectoryPath);
            if (!Directory.Exists(_modsTempDirectoryPath)) Directory.CreateDirectory(_modsTempDirectoryPath);
            string[] tempFilePaths = Directory.GetFiles(_modsTempDirectoryPath);

            if (final)
            {
                // All files in mods directory?
                foreach (Mod mod in Mods)
                {
                    string? matchingFile = (from p in modFilePaths where p.ToLower().EndsWith(mod.FileName.ToLower()) select p).SingleOrDefault();
                    if (matchingFile != null) continue;

                    matchingFile = (from p in tempFilePaths where p.ToLower().EndsWith(mod.FileName.ToLower()) select p).SingleOrDefault();
                    if (matchingFile == null) throw new MissingModFileException(mod.FileName);

                    FileInfo fileInfo = new FileInfo(matchingFile);
                    string targetFilePath = Path.Combine(ModsDirectoryPath, fileInfo.Name);

                    File.Move(matchingFile, targetFilePath);
                }
                // No deprecated files in mods directory?
                foreach (string filePath in modFilePaths)
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Mod? matchingMod = (from m in Mods where m.FileName.ToLower().Equals(fileInfo.Name.ToLower()) select m).SingleOrDefault();

                    if (matchingMod == null) File.Delete(fileInfo.FullName);
                }
                // Clearing temp directory
                tempFilePaths = Directory.GetFiles(_modsTempDirectoryPath);
                foreach (string filePath in tempFilePaths)
                {
                    File.Delete(filePath);
                }
            }
            else
            {
                // No deleted mods in mods directory?
                foreach (string filePath in modFilePaths)
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Mod? matchingMod = (from m in Mods where m.FileName.ToLower().Equals(fileInfo.Name.ToLower()) select m).SingleOrDefault();

                    string targetFilePath = Path.Combine(_modsTempDirectoryPath, fileInfo.Name);
                    if (matchingMod == null) File.Move(fileInfo.FullName, targetFilePath);
                }
            }
        }

        public void DeleteMod(Mod mod)
        {
            Mods.Remove(mod);
            CheckModFiles();
        }
        #endregion

        #region Methods PRIVATE
        private void AddMod(string filePath)
        {
            if (!File.Exists(filePath)) return;

            FileInfo fileInfo = new FileInfo(filePath);
            modDesc? modDescription;
            string? iconFileName = null;
            using (ZipArchive archive = ZipFile.OpenRead(filePath))
            {
                // modDesc.xml Deserialization
                {
                    ZipArchiveEntry? entry = archive.GetEntry("modDesc.xml");
                    if (entry == null) throw new InvalidFsModFileException(fileInfo.Name);

                    using (Stream stream = entry.Open())
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(modDesc));
                        modDescription = (modDesc?)serializer.Deserialize(reader);
                    }
                    if (modDescription == null) throw new InvalidFsModFileException(fileInfo.Name);
                }

                // icon.dds
                if (modDescription.iconFilename.EndsWith(".dds"))
                {
                    ZipArchiveEntry? entry = archive.GetEntry(modDescription.iconFilename);
                    if (entry != null)
                    {
                        iconFileName = $"icon_{Path.GetFileNameWithoutExtension(fileInfo.FullName)}.dds";
                        string iconFilePath = Path.Combine(ModIconsDirectoryPath, iconFileName);
                        if (!Directory.Exists(ModIconsDirectoryPath)) Directory.CreateDirectory(ModIconsDirectoryPath);
                        entry.ExtractToFile(iconFilePath, true);
                    }
                }
            }

            // Mod object generation
            Mod newMod = new Mod(this, modDescription.title.en, fileInfo.Name)
            {
                _version = modDescription.version,
                _author = modDescription.author,
                _description = modDescription.description.en.Trim(),
                _isMultiplayerCompatible = modDescription.multiplayer.supported,
                _imageSource = iconFileName,
            };

            // Mod file copying
            string targetFilePath = Path.Combine(ModsDirectoryPath, fileInfo.Name);
            if (!Directory.Exists(ModsDirectoryPath)) Directory.CreateDirectory(ModsDirectoryPath);
            File.Copy(fileInfo.FullName, targetFilePath, true);

            Mods.Add(newMod);
        }
        #endregion
    }
}
