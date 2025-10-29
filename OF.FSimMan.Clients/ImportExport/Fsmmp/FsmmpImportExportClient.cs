using OF.Base.Client;
using OF.FSimMan.Game;
using OF.FSimMan.Game.Exceptions;
using OF.FSimMan.ImportExport.Fsmmp.Exceptions;
using OF.FSimMan.Utility;
using System.IO.Compression;

namespace OF.FSimMan.Client.ImportExport.Fsmmp
{
    public class FsmmpImportExportClient : ClientBase, IImportExportClient<ModPack>
    {
        #region Properties
        private readonly FSimMan.Management.Game _game;
        #endregion

        #region Constructor
        public FsmmpImportExportClient(FSimMan.Management.Game game)
        {
            _game = game;
        }
        #endregion

        #region Methods PUBLIC
        public void Export(string targetFilePath, ModPack modPack)
        {
            try
            {
                IsBusy = true;

                ModPackData modPackData = new ModPackData();
                modPackData.ToData(modPack);
                using (FileStream fileStream = File.Create(targetFilePath))
                {
                    ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Create);

                    WriteModPackInfo(ref archive, modPackData);
                    WriteModPackData(ref archive, modPack);

                    archive.Dispose();
                }
            }
            finally
            {
                ResetBusyIndicator();
            }
        }

        public static Guid GetModPackGuid(string sourceFilePath)
        {
            string sourceFileName = Path.GetFileName(sourceFilePath);
            if (!File.Exists(sourceFilePath)) throw new InvalidModPackFileException(sourceFileName);

            ZipArchive archive = ZipFile.OpenRead(sourceFilePath);

            ModPackData modPackData = ReadModPackInfo(ref archive, sourceFileName);
            return modPackData.Guid;
        }

        public ModPack Import(string sourceFilePath, bool importAsNew)
        {
            try
            {
                IsBusy = true;

                string sourceFileName = Path.GetFileName(sourceFilePath);
                if (!File.Exists(sourceFilePath)) throw new InvalidModPackFileException(sourceFileName);

                ZipArchive archive = ZipFile.OpenRead(sourceFilePath);

                ModPackData modPackData = ReadModPackInfo(ref archive, sourceFileName);
                if (!modPackData.Game.Equals(_game)) throw new InvalidModPackFileException(sourceFileName);
                modPackData.Id = 0;
                if (importAsNew) modPackData.Guid = Guid.NewGuid();

                ModPack modPack = modPackData.FromData();
                modPack.Tags.Set(ModPackTag.Imported);
                ReadModPackData(ref archive, modPack);

                ModPackEditor modPackEditor = new ModPackEditor(modPack);
                modPackEditor.CheckIntegrity();
                modPackEditor.TriggerEndEdit();
                archive.Dispose();
                return modPack;
            }
            finally
            {
                ResetBusyIndicator();
            }
        }
        #endregion

        #region Methods PRIVATE
        private void WriteModPackInfo(ref ZipArchive archive, ModPackData modPackData)
        {
            ZipArchiveEntry entry = archive.CreateEntry("modPack.xml");
            Stream entryStream = entry.Open();
            FileSerializationHelper.Serialize(ref entryStream, modPackData);
            entryStream.Flush();
            entryStream.Close();
        }

        private void WriteModPackData(ref ZipArchive archive, ModPack modPack)
        {
            foreach (Mod mod in modPack.Mods)
            {
                string sourceModFilePath = Path.Combine(modPack.ModsDirectoryPath, mod.FileName);
                string targetModFilePath = $@"mods\{mod.FileName}";
                archive.CreateEntryFromFile(sourceModFilePath, targetModFilePath);

                string? sourceModIconFilePath = mod.FullImageSource;
                if (!string.IsNullOrWhiteSpace(sourceModIconFilePath))
                {
                    string targetModIconFilePath = $@"modIcons\{mod.ImageSource}";
                    archive.CreateEntryFromFile(sourceModIconFilePath, targetModIconFilePath);
                }
            }
        }

        private static ModPackData ReadModPackInfo(ref ZipArchive archive, string archiveFileName)
        {
            ZipArchiveEntry? entry = archive.GetEntry("modPack.xml");
            if (entry is null) throw new InvalidModPackFileException(archiveFileName);

            Stream entryStream = entry.Open();
            return FileSerializationHelper.Deserialize<ModPackData>(ref entryStream);
        }

        private void ReadModPackData(ref ZipArchive archive, ModPack modPack)
        {
            foreach (Mod mod in modPack.Mods)
            {
                mod.Id = 0;

                {
                    string sourceModFilePath = $@"mods\{mod.FileName}";
                    ZipArchiveEntry? entry = archive.GetEntry(sourceModFilePath);
                    if (entry is null) throw new InvalidModFileException(mod.FileName);

                    string targetModFilePath = Path.Combine(modPack.ModsDirectoryPath, mod.FileName);
                    entry.ExtractToFile(targetModFilePath, true);
                }

                if (!string.IsNullOrWhiteSpace(mod.ImageSource))
                {
                    string sourceModIconFilePath = $@"modIcons\{mod.ImageSource}";
                    ZipArchiveEntry? entry = archive.GetEntry(sourceModIconFilePath);
                    if (entry is not null)
                    {
                        string targetModIconFilePath = mod.FullImageSource!;
                        entry.ExtractToFile(targetModIconFilePath, true);
                    }
                }
            }
        }
        #endregion
    }
}
