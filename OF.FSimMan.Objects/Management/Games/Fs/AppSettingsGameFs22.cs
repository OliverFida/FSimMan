using OF.Base.Objects;
using OF.FSimMan.Management.Exceptions;

namespace OF.FSimMan.Management.Games.Fs
{
    public class AppSettingsGameFs22 : AppSettingsGameFsBase
    {
        #region Constructor
        public AppSettingsGameFs22() : base(Game.FarmingSim22) { }
        #endregion

        #region Methods PUBLIC
        public override void ValidateExeDirectoryPath(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (file.EndsWith("FarmingSimulator2022.exe")) return;
            }
            throw new GamePathIncorrectException();
        }

        public override void ValidateDataDirectoryPath(string path)
        {
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (file.EndsWith("gameSettings.xml")) return;
            }
            throw new DataPathIncorrectException();
        }
        #endregion
    }
}
