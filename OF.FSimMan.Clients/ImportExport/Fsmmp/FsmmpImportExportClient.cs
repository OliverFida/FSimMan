using OF.Base.Client;
using OF.FSimMan.Game;
using OF.FSimMan.Utility;
using System.IO.Compression;

namespace OF.FSimMan.Client.ImportExport.Fsmmp
{
    public class FsmmpImportExportClient : ClientBase, IImportExportClient<ModPack>
    {
        #region Methods PUBLIC
        public void Export(string targetFilePath, ModPack modPack)
        {
            try
            {
                IsBusy = true;

                string archiveFileName = Path.GetFileName(targetFilePath);

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

        public void Import(string sourceFilePath)
        {

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
        #endregion
    }
}
