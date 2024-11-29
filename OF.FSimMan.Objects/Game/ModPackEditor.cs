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
        public override void CancelEdit()
        {
            base.CancelEdit();
            CheckIntegrity(true);
        }

        public override void EndEdit()
        {
            base.EndEdit();
            CheckIntegrity(true);
        }

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
            File.Copy(fileInfo.FullName, targetFilePath, true);

            ObjectToEdit._mods.Add(newMod);
        }

        private void CheckModsIntegrity(bool final)
        {
            string[] modFilePaths = Directory.GetFiles(ObjectToEdit.ModsDirectoryPath);
            string[] tempFilePaths = Directory.GetFiles(ObjectToEdit.ModsTempDirectoryPath);

            if (final)
            {
                // All files in mods directory?
                foreach (Mod mod in ObjectToEdit.Mods)
                {
                    string? matchingFile = (from p in modFilePaths where p.ToLower().EndsWith(mod.FileName.ToLower()) select p).SingleOrDefault();
                    if (matchingFile is not null) continue; // Is in mods

                    matchingFile = (from p in tempFilePaths where p.ToLower().EndsWith(mod.FileName.ToLower()) select p).SingleOrDefault();
                    if (matchingFile is null) throw new MissingModFileException(mod.FileName); // Is not in tempMods

                    // Is in tempMods
                    FileInfo fileInfo = new FileInfo(matchingFile);
                    string targetFilePath = Path.Combine(ObjectToEdit.ModsDirectoryPath, fileInfo.Name);

                    File.Move(matchingFile, targetFilePath);
                }
                
                // No deprecated files in mods directory?
                foreach (string filePath in modFilePaths)
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Mod? matchingMod = (from m in ObjectToEdit.Mods where m.FileName.ToLower().Equals(fileInfo.Name.ToLower()) select m).SingleOrDefault();

                    if (matchingMod is null) File.Delete(fileInfo.FullName); // deprecated mod
                }

                // Clearing temp directory
                tempFilePaths = Directory.GetFiles(ObjectToEdit.ModsTempDirectoryPath);
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
                    Mod? matchingMod = (from m in ObjectToEdit.Mods where m.FileName.ToLower().Equals(fileInfo.Name.ToLower()) select m).SingleOrDefault();

                    if (matchingMod is null)
                    {
                        string targetFilePath = Path.Combine(ObjectToEdit.ModsTempDirectoryPath, fileInfo.Name);
                        File.Move(fileInfo.FullName, targetFilePath);
                    }
                }
            }
        }
        #endregion
    }
}
