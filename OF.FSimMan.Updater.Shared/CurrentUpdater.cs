namespace OF.FSimMan.Updater
{
    public static class CurrentUpdater
    {
        #region Properties
        private static readonly string[] _EXPECTABLE_START_ARGS =
        {
            "ghusername",
            "ghrepo",
            "currentversion"
        };

        private static Dictionary<string, string?> _START_ARGS = new Dictionary<string, string?>();
        public static Dictionary<string, string?> START_ARGS
        {
            get => _START_ARGS;
        }

        public static string TEMP_PATH
        {
            get
            {
                string temp = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Oliver Fida", "AutoUpdate", "temp");
                if (!Directory.Exists(temp)) Directory.CreateDirectory(temp);
                return temp;
            }
        }
        #endregion

        #region Methods PUBLIC
        public static void ParseStartArgs(string[] args)
        {
            Dictionary<string, string?> result = new Dictionary<string, string?>();

            foreach (string arg in args)
            {
                if (!arg.StartsWith("--")) continue;

                string[] parts = arg.Substring(2).Split('=', 2);

                string key = parts[0].ToLower();
                if (!_EXPECTABLE_START_ARGS.Contains(key)) continue;
                if (key.Equals("currentversion"))
                {
                    ParseStartArgsCurrentVersion(ref result, parts[1]);
                    continue;
                }

                string? value = parts.Length > 1 ? parts[1] : null;

                result[key] = value;
            }

            _START_ARGS = result;
        }
        #endregion

        #region Methods PRIVATE
        private static void ParseStartArgsCurrentVersion(ref Dictionary<string, string?> result, string currentVersion)
        {
            string[] parts = currentVersion.Split('.');
            if (parts.Length != 3) throw new ArgumentException("Version must be give in format 'x.x.x'");

            result["currentmajor"] = parts[0];
            result["currentminor"] = parts[1];
            result["currentbuild"] = parts[2];
        }
        #endregion
    }
}
