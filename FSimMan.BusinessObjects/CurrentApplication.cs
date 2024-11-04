﻿using OliverFida.FSimMan.Config;
using System.Reflection;

namespace OliverFida.FSimMan
{
    public static class CurrentApplication
    {
        #region Properties
        public static AppSettings? AppSettings { get; private set; }

        private static readonly string _appTitleBase = "FSimMan";
        public static string AppTitleBase
        {
            get => _appTitleBase;
        }

        private static Version? _assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
        public static string AssemblyVersionText
        {
            get => $"v{_assemblyVersion}";
        }
        public static string AppTitle
        {
            get
            {
#if DEBUG
                return $"{_appTitleBase} (development)";
#endif
                if (_assemblyVersion != null) return $"{_appTitleBase} {AssemblyVersionText}";
                return _appTitleBase;
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
                return Path.Combine(temp, "config");
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
                return Path.Combine(temp, "modPacks");
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
                return Path.Combine(temp, "temp");
            }
        }
        #endregion

        #region Methods INTERNAL
        public static void Initialize(AppSettings appSettings)
        {
#if DEBUG
            ReleaseFeatures.InitializeDebugValues();
#endif
            AppSettings = appSettings;
        }
        #endregion
    }
}