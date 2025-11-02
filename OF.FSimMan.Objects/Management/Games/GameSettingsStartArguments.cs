namespace OF.FSimMan.Management.Games
{
    public class GameSettingsStartArguments : AppSettingsBase
    {
        #region Constants
        private const string _argumentPrefix = "-";
        #endregion

        #region Properties
        internal bool _skipIntros = false;
        public bool SkipIntros
        {
            get => _skipIntros;
            set => SetProperty(ref _skipIntros, value);
        }

        internal bool _disableFrameLimit = false;
        public bool DisableFrameLimit
        {
            get => _disableFrameLimit;
            set => SetProperty(ref _disableFrameLimit, value);
        }

        internal bool _enableCheats = false;
        public bool EnableCheats
        {
            get => _enableCheats;
            set => SetProperty(ref _enableCheats, value);
        }
        #endregion

        #region Methods PUBLIC
        public string GetArgumentsString()
        {
            return string.Join(" ", GetArgumentsList());
        }
        #endregion

        #region Methods PRIVATE
        private List<string> GetArgumentsList()
        {
            List<string> temp = new List<string>();

            if (SkipIntros) temp.Add($"{_argumentPrefix}skipStartVideos");
            // OFDO: if (DisableFrameLimit) temp.Add($"{_argumentPrefix}disableFrameLimiter"); // Apperently won't work anymore from FS22 upwards
            if (EnableCheats) temp.Add($"{_argumentPrefix}cheats");
            // OFDO: if (EnableScriptDebug) temp.Add($"{_argumentPrefix}scriptDebug");
            // OFDO: if (DisableShaderCompiler) temp.Add($"{_argumentPrefix}disableShaderCompiler");

            return temp;
        }
        #endregion
    }
}
