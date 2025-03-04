namespace OF.FSimMan.Management
{
    public abstract class GameInfoBase
    {
        #region Properties
        public readonly Game Game;

        public readonly string Title;

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
        #endregion

        #region Constructor
        public GameInfoBase(Game game, string title, string imageFileName, string exeName)
        {
            Game = game;
            Title = title;
            _imageFileName = imageFileName;
            ExeName = exeName;
        }
        #endregion
    }
}
