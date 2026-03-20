using System.IO;
using System.Windows;

namespace OF.FSimMan.Updater
{
    public partial class App : Application
    {
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

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

#if DEBUG
            string[] debugArgs = {
                "--ghusername=OliverFida",
                "--ghrepo=FSimMan",
                "--currentversion=0.0.1"
            };
            _START_ARGS = ParseStartArgs(debugArgs);
#else
            _START_ARGS = ParseStartArgs(e.Args);
#endif
        }

        private static Dictionary<string, string?> ParseStartArgs(string[] args)
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

            return result;
        }

        private static void ParseStartArgsCurrentVersion(ref Dictionary<string, string?> result, string currentVersion)
        {
            string[] parts = currentVersion.Split('.');
            if (parts.Length != 3) throw new ArgumentException("Version must be give in format 'x.x.x'");

            result["currentmajor"] = parts[0];
            result["currentminor"] = parts[1];
            result["currentbuild"] = parts[2];
        }
    }
}
