using OF.Base.Objects;
using System.Reflection;

namespace OF.FSimMan
{
    public class CurrentApplication : ISingleton<CurrentApplication>
    {
        #region ISingleton
        private static CurrentApplication _instance = new CurrentApplication();
        public static CurrentApplication Instance
        {
            get => _instance;
        }
        #endregion

        #region Properties
        private static readonly Version? _assemblyVersion = Assembly.GetEntryAssembly()!.GetName().Version;
        public static Version? AssemblyVersion
        {
            get => _assemblyVersion;
        }
        public static string AssemblyVersionText
        {
            get => $"v{_assemblyVersion?.Major}.{_assemblyVersion?.Minor}.{_assemblyVersion?.Build}";
        }

        public static string WindowTitleBase
        {
            get => "FSimMan";
        }

        public static string WindowTitle
        {
            get
            {
#if DEBUG
                return $"{WindowTitleBase} (development)";
#else
                if (AssemblyVersion != null) return $"{WindowTitleBase} {AssemblyVersionText}";
                return WindowTitleBase;
#endif
            }
        }

        public static string CONFIG_PATH
        {
            get
            {
                string temp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Oliver Fida", "FSimMan");
#if DEBUG
                temp = Path.Combine(temp, "_debug");
#endif
                temp = Path.Combine(temp, "config");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }

        public static string MODPACKS_PATH
        {
            get
            {
                string temp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Oliver Fida", "FSimMan");
#if DEBUG
                temp = Path.Combine(temp, "_debug");
#endif
                temp = Path.Combine(temp, "modPacks");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }

        public static string TEMP_PATH
        {
            get
            {
                string temp = Path.Combine($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}Low", "Oliver Fida", "FSimMan");
#if DEBUG
                temp = Path.Combine(temp, "_debug");
#endif
                temp = Path.Combine(temp, "temp");
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
    }
}
