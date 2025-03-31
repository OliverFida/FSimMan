using OF.Base.Objects;
using System.Collections.ObjectModel;

namespace OF.FSimMan.Management
{
    public class GameInfoCollection : ISingleton<GameInfoCollection>
    {
        #region ISingleton
        private static GameInfoCollection _instance = new GameInfoCollection();
        public static GameInfoCollection Instance
        {
            get => _instance;
        }
        #endregion

        #region Properties
        private List<GameInfoBase> _list = new List<GameInfoBase>();
        #endregion

        #region Constructor
        private GameInfoCollection()
        {
            AddGameInfo(new Fs22GameInfo());
            AddGameInfo(new Fs25GameInfo());
        }
        #endregion

        #region Methods PUBLIC
        public GameInfoBase GetGameInfo(Game game)
        {
            return TryGetGameInfo(game) ?? throw new NotImplementedException();
        }

        public ReadOnlyCollection<GameInfoBase> GetAll()
        {
            return new ReadOnlyCollection<GameInfoBase>(_list);
        }
        #endregion

        #region Methods PRIVATE
        private GameInfoBase? TryGetGameInfo(Game game)
        {
            return _list.Find(i => i.Game.Equals(game));
        }

        private void AddGameInfo(GameInfoBase gameInfo)
        {
            if (TryGetGameInfo(gameInfo.Game) is not null) return;

            _list.Add(gameInfo);
        }
        #endregion
    }
}
