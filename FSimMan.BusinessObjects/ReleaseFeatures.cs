namespace OliverFida.FSimMan
{
    public static class ReleaseFeatures
    {
        public static bool ApplicationModeCreator { get; private set; } = false;
        public static bool ModPackImportExport { get; private set; } = false;
        public static bool ModPackHub { get; private set; } = false;
        public static bool GameFs25 { get; private set; } = false;

#if DEBUG
        public static void InitializeDebugValues()
        {
            ApplicationModeCreator = true;
            ModPackImportExport = true;
            ModPackHub = true;
            GameFs25 = true;
        }
#endif
    }
}
