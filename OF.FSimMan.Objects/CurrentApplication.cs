using OF.FSimMan.Base;
using System.Reflection;

namespace OF.FSimMan
{
    public class CurrentApplication : ICustomSingleton<CurrentApplication>
    {
        #region ISingleton
        private static CurrentApplication _instance = new CurrentApplication();
        public static CurrentApplication Instance
        {
            get => _instance;
        }
        #endregion

        #region Properties
#if DEBUG
        public static LaunchMode LaunchMode = LaunchMode.Default;
#endif

        private static readonly Version? _assemblyVersion = Assembly.GetEntryAssembly()!.GetName().Version;
        public static Version? AssemblyVersion
        {
            get => _assemblyVersion;
        }
        public static string AssemblyVersionText
        {
            get
            {
#if DEBUG
                if (_assemblyVersion is not null &&
                    _assemblyVersion.Major.Equals(0) &&
                    _assemblyVersion.Minor.Equals(0) &&
                    _assemblyVersion.Build.Equals(0)) return "vDev";
                else
                    return $"v{_assemblyVersion?.Major}.{_assemblyVersion?.Minor}.{_assemblyVersion?.Build}.{_assemblyVersion?.Revision}";
#else
                return $"v{_assemblyVersion?.Major}.{_assemblyVersion?.Minor}.{_assemblyVersion?.Build}";
#endif
            }
        }

        public static string WindowTitleBase
        {
            get => "FSimMan";
        }

        public static string WindowTitle
        {
            get
            {
                if (AssemblyVersion != null) return $"{WindowTitleBase} {AssemblyVersionText}";
                return WindowTitleBase;
            }
        }

        private static string APPDATA_PATH
        {
            get
            {
                string temp = string.Empty;
                string pathInstalled = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Oliver Fida", "FSimMan");

#if DEBUG
                temp = pathInstalled;

                string debugSuffix = string.Empty;
                switch (LaunchMode)
                {
                    case LaunchMode.UnitTests:
                        debugSuffix = "-ut";
                        break;
                    case LaunchMode.ManualTesting:
                        DirectoryInfo dirInfo = new DirectoryInfo(AppContext.BaseDirectory);
                        debugSuffix = $"-mt-{dirInfo.Name}";
                        break;
                }
                temp = Path.Combine(temp, $"_debug{debugSuffix}");
#else
                if (GetIsRunningAsInstalled()) temp = pathInstalled;
                else temp = Path.Combine(AppContext.BaseDirectory, "data");
#endif
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }

        public static string CONFIG_PATH
        {
            get
            {
                string temp = APPDATA_PATH;
                temp = Path.Combine(temp, "config");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }

        public static string CONFIG_BACKUP_PATH
        {
            get
            {
                string temp = APPDATA_PATH;
                temp = Path.Combine(temp, "config_backups");
                if (!Directory.Exists(temp))
                {
                    DirectoryInfo dirInfo = Directory.CreateDirectory(temp);
                    dirInfo.Attributes = dirInfo.Attributes | FileAttributes.Hidden;
                }
                return temp;
            }
        }

        public static string CONFIG_DATABASE_PATH
        {
            get
            {
                string temp = Path.Combine(CONFIG_PATH, "config.db");
                return temp;
            }
        }

        public static string MODPACKS_PATH
        {
            get
            {
                string temp = APPDATA_PATH;
                temp = Path.Combine(temp, "modPacks");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }
        #endregion

        #region Constructor
        private CurrentApplication()
        {
#if DEBUG
            ReleaseFeatures.InitializeDebugValues();
#endif
        }
        #endregion

        #region Methods PUBLIC
        public static string GetModPackDatabasePath(Management.Game game)
        {
            return Path.Combine(CONFIG_PATH, $"modPacks{game.ToString()}.db");
        }
        #endregion

        #region Methods PRIVATE
        private static bool GetIsRunningAsInstalled()
        {
            try
            {
                string testFilePath = Path.Combine(AppContext.BaseDirectory, "testIsInstalled.tmp");
                File.WriteAllText(testFilePath, "test");
                File.Delete(testFilePath);
                return false;
            }
            catch
            {
                return true;
            }
        }
        #endregion
    }
}
