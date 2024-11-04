using OliverFida.FSimMan.Config.ModPack;
using OliverFida.FSimMan.Exceptions;
using OliverFida.FSimMan.Exceptions.Fs;
using System.IO.Compression;
using System.Xml.Serialization;

namespace OliverFida.FSimMan.ImportExport
{
    public class FsmmpFile : IDisposable
    {
        #region Properties
        private bool _isNew = false;
        private string _archiveFilePath;
        private string _archiveFileName;
        private ZipArchive _archive;

        private ModPackData? _modPackData;
        public ModPackData? ModPackData
        {
            get => _modPackData;
        }

        private ModPack? _modPack;
        public ModPack? ModPack
        {
            get => _modPack;
        }

        private string _modsDirectoryPath;
        #endregion

        #region Constructor & Finalizer
        public FsmmpFile(string archiveFilePath)
        {
            _archiveFilePath = archiveFilePath;
            _archiveFileName = Path.GetFileName(_archiveFilePath);
            ValidateFilePath();

            _archive = ZipFile.OpenRead(_archiveFilePath);
            ReadModPackData();
            _modPack = _modPackData!.FromData();
            _modsDirectoryPath = _modPack.ModsDirectoryPath;
            if (!Directory.Exists(_modsDirectoryPath)) Directory.CreateDirectory(_modsDirectoryPath);
        }

        public FsmmpFile(string archiveFilePath, ModPack modPack)
        {
            _isNew = true;
            _archiveFilePath = archiveFilePath;
            _archiveFileName = Path.GetFileName(_archiveFilePath);

            _modPack = modPack;
            _modPackData = new ModPackData();
            _modPackData.ToData(_modPack);
            _modsDirectoryPath = _modPack.ModsDirectoryPath;
            using (FileStream fileStream = File.Create(_archiveFilePath))
            {
                _archive = new ZipArchive(fileStream, ZipArchiveMode.Create);
                WriteModPackData();
                ExportModPack();
                _archive.Dispose();
            }
        }
        #endregion

        #region Methods PUBLIC
        public void Dispose()
        {
            if (_isNew) return;
            _archive.Dispose();
        }

        public void ImportModPack()
        {
            foreach (Mod mod in ModPack!.Mods)
            {
                string sourceModFilePath = $@"mods\{mod.FileName}";
                ZipArchiveEntry? entry = _archive.GetEntry(sourceModFilePath);
                if (entry == null) throw new InvalidFsModFileException(mod.FileName);

                string targetModFilePath = Path.Combine(ModPack.ModsDirectoryPath, mod.FileName);
                entry.ExtractToFile(targetModFilePath, true);
            }
        }
        #endregion

        #region Methods PRIVATE
        private void ValidateFilePath()
        {
            if (!File.Exists(_archiveFilePath)) throw new InvalidModPackFileException(_archiveFileName);
        }

        private void ReadModPackData()
        {
            ZipArchiveEntry? entry = _archive.GetEntry("modPack.xml");
            if (entry == null) throw new InvalidModPackFileException(_archiveFileName);

            using (Stream stream = entry.Open())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ModPackData));
                _modPackData = serializer.Deserialize(stream) as ModPackData;
            }
            if (_modPackData == null) throw new InvalidModPackFileException(_archiveFileName);
        }

        private void WriteModPackData()
        {
            ZipArchiveEntry entry = _archive.CreateEntry("modPack.xml");
            Stream entryStream = entry.Open();
            XmlSerializer serializer = new XmlSerializer(typeof(ModPackData));
            serializer.Serialize(entryStream, _modPackData);
            entryStream.Flush();
            entryStream.Close();
        }

        private void ExportModPack()
        {
            foreach (Mod mod in ModPack!.Mods)
            {
                string sourceModFilePath = Path.Combine(_modPack!.ModsDirectoryPath, mod.FileName);
                string targetModFilePath = $@"mods\{mod.FileName}";
                _archive.CreateEntryFromFile(sourceModFilePath, targetModFilePath);
            }
        }
        #endregion
    }
}
