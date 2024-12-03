using OF.Base.Objects;
using OF.FSimMan.Game.Exceptions;
using OF.FSimMan.Game.Fs.Fs22.Mod;
using OF.FSimMan.Utility;
using System.IO.Compression;

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
            // OFDOL: object modsLock = new object();
            //Parallel.ForEach(filePaths, filePath =>
            //{
            //AddMod(filePath, modsLock);
            //});
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
            CheckModIconsIntegrity(final);
        }
        #endregion

        #region Methods PRIVATE
        // OFDOL: private void AddMod(string filePath, object lockObject)
        private void AddMod(string filePath)
        {
            if (!File.Exists(filePath)) return;

            FileInfo fileInfo = new FileInfo(filePath);
            CheckModIsMatchingGame(fileInfo);

            modDesc modDescription;
            string? iconFileName = null;

            using (ZipArchive archive = ZipFile.OpenRead(filePath))
            {
                // modDesc.xml
                {
                    ZipArchiveEntry? entry = archive.GetEntry("modDesc.xml");
                    if (entry is null) throw new InvalidModFileException(fileInfo.Name);

                    Stream stream = entry.Open();
                    modDescription = FileSerializationHelper.Deserialize<modDesc>(ref stream);
                    stream.Dispose();

                    if (modDescription.title is null) throw new InvalidModFileException(fileInfo.Name);
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

            // OFDOL: lock (lockObject)
            //{
            //    ObjectToEdit._mods.Add(newMod);
            //}
            ObjectToEdit._mods.Add(newMod);
        }

        private void CheckModIsMatchingGame(FileInfo fileInfo)
        {
            string fileName = fileInfo.Name.ToLower();

            switch (ObjectToEdit._game)
            {
                case Management.Game.FarmingSim22:
                    if (fileName.StartsWith("fs22")) return;
                    break;
                case Management.Game.FarmingSim25:
                    if (fileName.StartsWith("fs25")) return;
                    break;
                default:
                    throw new NotImplementedException();
            }

            throw new InvalidModFileException(fileInfo.Name);
        }

        private void CheckModsIntegrity(bool final)
        {
            string[] modFilePaths = Directory.GetFiles(ObjectToEdit.ModsDirectoryPath);
            string[] tempFilePaths = Directory.GetFiles(ObjectToEdit.ModsTempDirectoryPath);

            if (final)
            {
                // All files in mods directory?
                Parallel.ForEach(ObjectToEdit.Mods, mod =>
                {
                    string? matchingFile = (from p in modFilePaths where p.ToLower().EndsWith(mod.FileName.ToLower()) select p).SingleOrDefault();
                    if (matchingFile is not null) return; // Is in mods

                    matchingFile = (from p in tempFilePaths where p.ToLower().EndsWith(mod.FileName.ToLower()) select p).SingleOrDefault();
                    if (matchingFile is null) throw new MissingModFileException(mod.FileName); // Is not in tempMods

                    // Is in tempMods
                    FileInfo fileInfo = new FileInfo(matchingFile);
                    string targetFilePath = Path.Combine(ObjectToEdit.ModsDirectoryPath, fileInfo.Name);

                    File.Move(matchingFile, targetFilePath);
                });

                // No deprecated files in mods directory?
                Parallel.ForEach(modFilePaths, filePath =>
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Mod? matchingMod = (from m in ObjectToEdit.Mods where m.FileName.ToLower().Equals(fileInfo.Name.ToLower()) select m).SingleOrDefault();

                    if (matchingMod is null) File.Delete(fileInfo.FullName); // deprecated mod
                });

                // Clearing temp directory
                tempFilePaths = Directory.GetFiles(ObjectToEdit.ModsTempDirectoryPath);
                Parallel.ForEach(tempFilePaths, filePath =>
                {
                    File.Delete(filePath);
                });
            }
            else
            {
                // No deleted mods in mods directory?
                Parallel.ForEach(modFilePaths, filePath =>
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Mod? matchingMod = (from m in ObjectToEdit.Mods where m.FileName.ToLower().Equals(fileInfo.Name.ToLower()) select m).SingleOrDefault();

                    if (matchingMod is null)
                    {
                        string targetFilePath = Path.Combine(ObjectToEdit.ModsTempDirectoryPath, fileInfo.Name);
                        File.Move(fileInfo.FullName, targetFilePath);
                    }
                });
            }
        }

        private void CheckModIconsIntegrity(bool final)
        {
            if (!final) return;

            string[] iconFilePaths = Directory.GetFiles(ObjectToEdit.ModIconsDirectoryPath);

            // No deprecated files in icons directory?
            Parallel.ForEach(iconFilePaths, filePath =>
            {
                FileInfo fileInfo = new FileInfo(filePath);
                Mod? matchingMod = (from m in ObjectToEdit.Mods where m.FileName.ToLower().Replace(".zip", "").Equals(fileInfo.Name.ToLower().Replace("icon_", "").Replace(".dds", "")) select m).SingleOrDefault();

                if (matchingMod is null) File.Delete(fileInfo.FullName); // deprecated icon
            });
        }
        #endregion
    }
}
