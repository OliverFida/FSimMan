namespace OF.FSimMan
{
    public static class ReleaseFeatures
    {
        public static bool ApplicationModeCreator { get; private set; } = true;
        public static bool ModPackImportExport { get; private set; } = true;
        public static bool GiantsModPackHub { get; private set; } = false;
        public static bool GameFs25 { get; private set; } = false;

#if DEBUG
        public static void InitializeDebugValues()
        {
            ApplicationModeCreator = true;
            ModPackImportExport = true;
            GiantsModPackHub = true;
            GameFs25 = true;
        }
#endif
    }
}
