using OF.Base.Objects;
using OF.FSimMan.Game.Exceptions;
using OF.FSimMan.Game.Fs.Fs22.Mod;
using OF.FSimMan.Utility;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;

namespace OF.FSimMan.Game
{
    public class ModPackEditor : EditorBase<ModPack>
    {
        #region Constructor & Initialize
        public ModPackEditor(ModPack objectToEdit) : base(objectToEdit)
        {
            ObjectToEdit.BeginEdit();
        }
        #endregion

        #region Methods PUBLIC
        public void AddMods(string[] filePaths)
        {
            foreach (string filePath in filePaths)
            {
                AddMod(filePath);
            }
            CheckIntegrity();
        }

        public void RemoveMod(Mod mod)
        {
            ObjectToEdit._mods.Remove(mod);
            CheckIntegrity();
        }

        public void CheckIntegrity() => CheckIntegrity(false);
        public void CheckIntegrity(bool final)
        {
            CheckModsIntegrity(final);
        }
        #endregion

        #region Methods PRIVATE
        private void AddMod(string filePath)
        {
            if (!File.Exists(filePath)) return;

            FileInfo fileInfo = new FileInfo(filePath);
            modDesc? modDescription = null;
            string? iconFileName = null;

            using (ZipArchive archive = ZipFile.OpenRead(filePath))
            {
                // modDesc.xml
                {
                    ZipArchiveEntry? entry = archive.GetEntry("modDesc.xml");
                    if (entry is null) throw new InvalidModFileException(fileInfo.Name);

                    // OFDO: FileSerializationHelper.Deserialize<modDesc>()
                    using (Stream stream = entry.Open())
                    using (XmlReader reader = XmlReader.Create(stream))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(modDesc));
                        modDescription = (modDesc?)serializer.Deserialize(reader);
                    }
                    if (modDescription is null) throw new InvalidModFileException(fileInfo.Name);
                }

                // icon.dds
                if (modDescription.iconFilename.EndsWith(".dds"))
                {
                    ZipArchiveEntry? entry = archive.GetEntry(modDescription.iconFilename);
                    if (entry is not null)
                    {
                        iconFileName = $"icon_{Path.GetFileNameWithoutExtension(fileInfo.FullName)}.dds";
                        string iconFilePath = Path.Combine(ObjectToEdit.ModIconsDirectoryPath, iconFileName);
                        if (!Directory.Exists(ObjectToEdit.ModIconsDirectoryPath)) Directory.CreateDirectory(ObjectToEdit.ModIconsDirectoryPath);
                        entry.ExtractToFile(iconFilePath, true);
                    }
                }
            }

            // Mod object generation
            Mod newMod = new Mod(ObjectToEdit, fileInfo.Name)
            {
                _title = modDescription.title.en.Trim(),
                _version = modDescription.version.Trim(),
                _author = modDescription.author.Trim(),
                _description = modDescription.description.en.Trim(),
                _isMultiplayerCompatible = modDescription.multiplayer.supported,
                _imageSource = iconFileName,
            };

            // Mod file copying
            string targetFilePath = Path.Combine(ObjectToEdit.ModsDirectoryPath, fileInfo.Name);
            if (!Directory.Exists(ObjectToEdit.ModsDirectoryPath)) Directory.CreateDirectory(ObjectToEdit.ModsDirectoryPath);
            File.Copy(fileInfo.FullName, targetFilePath, true);

            ObjectToEdit._mods.Add(newMod);
        }

        private void CheckModsIntegrity(bool final)
        {
            // OFDO
        }
        #endregion
    }
}
