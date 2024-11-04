using OliverFida.FSimMan.Base;
using OliverFida.FSimMan.Client.ModPack;
using OliverFida.FSimMan.Config.ModPack;
using OliverFida.FSimMan.Exceptions.Fs;
using OliverFida.FSimMan.FS22;
using System.Diagnostics;
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


        protected ModPacks? _configModPacks;
        public ModPacks? ConfigModPacks
        {
            get => _configModPacks;
        }
        #endregion

        #region Constructor & Initialize
        public FsBaseClient(FsEdition fsEdition)
        {
            _fsEdition = fsEdition;

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
            if (_configModPacks == null) return;

            bool modPacksIsEditing = ConfigModPacks?.IsEditing ?? false;
            if (modPacksIsEditing) ConfigModPacks!.EndEdit();
            ModPacksClient.StoreModPacks(FsEdition, _configModPacks);
            if (modPacksIsEditing) ConfigModPacks!.BeginEdit();
        }

        public Config.ModPack.ModPack? BeginNewModPack()
        {
            if (ConfigModPacks == null) return null;

            ConfigModPacks.BeginEdit();
            Config.ModPack.ModPack newModPack = new Config.ModPack.ModPack("New modpack", "unknown", _fsEdition);
            ConfigModPacks.AddModPack(newModPack);

            return newModPack;
        }

        public void DeleteModPack(Config.ModPack.ModPack modPack)
        {
            if (ConfigModPacks == null) return;

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
