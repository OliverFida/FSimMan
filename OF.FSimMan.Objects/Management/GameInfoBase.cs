namespace OF.FSimMan.Management
{
    public abstract class GameInfoBase
    {
        #region Properties
        public readonly Game Game;

        public string Title { get; }

        private readonly string _imageFileName;
        private const string _imageBasePath = "pack://application:,,,/OF.FSimMan.Resources;component/Logos/";
        public string ImageFilePath
        {
            get
            {
                return _imageBasePath + _imageFileName;
            }
        }

        public readonly string ExeName;
        public readonly string ProcessName;
        public readonly string? LauncherProcessName;
        #endregion

        #region Constructor
        public GameInfoBase(Game game, string title, string imageFileName, string exeName, string processName) : this(game, title, imageFileName, exeName, processName, null) { }
        public GameInfoBase(Game game, string title, string imageFileName, string exeName, string processName, string? launcherProcessName)
        {
            Game = game;
            Title = title;
            _imageFileName = imageFileName;
            ExeName = exeName;
            ProcessName = processName;
            LauncherProcessName = launcherProcessName;
        }
        #endregion
    }
}
