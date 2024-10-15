using OliverFida.FSimMan.Client;
using System.Reflection;

namespace OliverFida.FSimMan
{
    internal static class CurrentApplication
    {
        #region Properties
        internal static AppSettingsClient AppSettingsClient { get; private set; } = new AppSettingsClient();

        private static readonly string _appTitleBase = "FSimMan";
        private static Version? _assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
        public static string AppTitle
        {
            get
            {
#if DEBUG
                return $"{_appTitleBase} (development)";
#endif
                if (_assemblyVersion != null) return $"{_appTitleBase} v{_assemblyVersion}";
                return _appTitleBase;
            }
        }
        #endregion
    }
}
