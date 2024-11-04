using OliverFida.FSimMan.Base;
using OliverFida.FSimMan.Client.ModPack;
using OliverFida.FSimMan.Config.ModPack;
using OliverFida.FSimMan.Exceptions;
using OliverFida.FSimMan.Exceptions.Fs;
using OliverFida.FSimMan.FS22;
using OliverFida.FSimMan.FS22.Mod;
using OliverFida.FSimMan.ImportExport;
using System.Diagnostics;
using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;

namespace OliverFida.FSimMan.Client
{
    public abstract class FsBaseClient : GameClientBase
    {
        #region Properties
        private const string FILE_NAME_GAMESETTINGS = "gameSettings.xml";

        private string GameSettingsFilePath
        {
            get
            {
                switch (FsEdition)
                {
                    case FsEdition.Fs22:
                        return Path.Combine(CurrentApplication.AppSettings!.Fs22DataPath, FILE_NAME_GAMESETTINGS);
                    case FsEdition.Fs25:
                        return Path.Combine(CurrentApplication.AppSettings!.Fs25DataPath, FILE_NAME_GAMESETTINGS);
                    default:
                        throw new NotImplementedException();
                }
            }
        }
        private gameSettings? _gameSettings;

        private FsEdition _fsEdition;
        public FsEdition FsEdition
        {
            get => _fsEdition;
        }


        protected ModPacks _configModPacks;
        public ModPacks ConfigModPacks
        {
            get => _configModPacks;
        }
        #endregion

        #region Constructor & Initialize
        public FsBaseClient(FsEdition fsEdition)
        {
            _fsEdition = fsEdition;
            _configModPacks = new ModPacks();

            Initialize();
        }

        private void Initialize()
        {
            // gameSettings.xml
            ReadGameSettings();

            // TODO: SaveGames
            //{                
            //    string[] saveGamePaths = Directory.GetDirectories(CurrentApplication.AppSettings.Fs22DataPath).Where(d => d.Contains("savegame") && !d.Contains("savegameBackup")).ToArray();
            //    _saveGames = (from p in saveGamePaths select new Fs22SaveGame(p)).ToList();
            //}

            // ModPacks
            RefreshModPacks();
        }
        #endregion

        #region Methods PUBLIC
        public void RefreshModPacks()
        {
            _configModPacks = ModPacksClient.ReadModPacks(FsEdition);
            OnPropertyChanged(nameof(ConfigModPacks));
        }

        public void StoreModPacks()
        {
            bool modPacksIsEditing = ConfigModPacks?.IsEditing ?? false;
            if (modPacksIsEditing) ConfigModPacks!.EndEdit();
            ModPacksClient.StoreModPacks(FsEdition, _configModPacks);
            if (modPacksIsEditing) ConfigModPacks!.BeginEdit();
        }

        public Config.ModPack.ModPack? BeginNewModPack()
        {
            ConfigModPacks.BeginEdit();
            Config.ModPack.ModPack newModPack = new Config.ModPack.ModPack("New modpack", "unknown", _fsEdition);
            ConfigModPacks.AddModPack(newModPack);

            return newModPack;
        }

        public bool ImportCheckModPackExists(FsmmpFile fsmmpFile)
        {
            Config.ModPack.ModPack? matchingModpack = (from p in ConfigModPacks.List where p.Key.Equals(fsmmpFile.ModPackData!.Key) select p).SingleOrDefault();
            if (matchingModpack != null) return true;

            return false;
        }

        public void ImportModPack(FsmmpFile fsmmpFile, bool overwrite)
        {
            try
            {
                ConfigModPacks.BeginEdit();
                fsmmpFile.ImportModPack();
                if (overwrite)
                {
                    ConfigModPacks.UpdateModPack(fsmmpFile.ModPack!);
                }
                else
                {
                    ConfigModPacks.AddModPack(fsmmpFile.ModPack!);
                }
                fsmmpFile.ModPack!.CheckModFiles();
                StoreModPacks();
                ConfigModPacks.EndEdit();
            }
            catch
            {
                ConfigModPacks.CancelEdit();
            }
        }

        public void ExportModPack(Config.ModPack.ModPack modPack, string targetArchiveFilePath)
        {
            using (FsmmpFile fsmmpFile = new FsmmpFile(targetArchiveFilePath, modPack)) { }
        }

        public void DeleteModPack(Config.ModPack.ModPack modPack)
        {
            ConfigModPacks.BeginEdit();
            ConfigModPacks.RemoveModPack(modPack);
            ConfigModPacks.EndEdit();
            StoreModPacks();

            RefreshModPacks();
        }

        public void RunGame(Config.ModPack.ModPack? modPack)
        {
            SetGameModFolder(modPack);
            ExecuteGameExe();
        }
        #endregion

        #region Methods PRIVATE
        private void SetGameModFolder(Config.ModPack.ModPack? modPack)
        {
            if (_gameSettings == null) return;

            gameSettingsModsDirectoryOverride modsDirOverride = new gameSettingsModsDirectoryOverride();
            if (modPack == null)
            {
                modsDirOverride.active = false;
                modsDirOverride.directory = string.Empty;
            }
            else
            {
                modsDirOverride.active = true;
                modsDirOverride.directory = Path.Combine(CurrentApplication.MODPACKS_PATH, FsEdition.ToString(), modPack.Key.ToString(), "mods");
            }

            _gameSettings.modsDirectoryOverride = modsDirOverride;
            StoreGameSettings();
        }

        private void ReadGameSettings()
        {
            gameSettings? gameSettings;
            using (XmlReader reader = XmlReader.Create(GameSettingsFilePath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(gameSettings));
                gameSettings = serializer.Deserialize(reader) as gameSettings;
            }
            if (gameSettings == null) throw new InvalidFsFileException(FILE_NAME_GAMESETTINGS);

            _gameSettings = gameSettings;
        }

        private void StoreGameSettings()
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
            namespaces.Add(string.Empty, string.Empty);

            FileStream fileStream = File.Create(GameSettingsFilePath);

            XmlSerializer serializer = new XmlSerializer(typeof(gameSettings));
            serializer.Serialize(fileStream, _gameSettings, namespaces);

            fileStream.Close();
        }

        private void ExecuteGameExe()
        {
            string filePath;
            switch (FsEdition)
            {
                case FsEdition.Fs22:
                    filePath = Path.Combine(CurrentApplication.AppSettings!.Fs22GamePath, "FarmingSimulator2022.exe");
                    break;
                case FsEdition.Fs25:
                    filePath = Path.Combine(CurrentApplication.AppSettings!.Fs25GamePath, "FarmingSimulator2025.exe");
                    break;
                default:
                    throw new NotImplementedException();
            }

            Process.Start(filePath);
        }
        #endregion
    }
}
