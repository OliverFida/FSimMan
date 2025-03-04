using Accessibility;
using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.FSimMan.Management;
using System.Collections.ObjectModel;

namespace OF.FSimMan.ViewModel
{
    public class GameRunningViewModel : ViewModelBase, ISingleton<GameRunningViewModel>
    {
        #region ISingleton
        private static readonly GameRunningViewModel _instance = new GameRunningViewModel();
        public static GameRunningViewModel Instance => _instance;
        #endregion

        #region Properties
        private bool _isStartPlanned = false;
        private bool _isStopPlanned = false;

        private GameInfoBase? _runningGame;
        public GameInfoBase? RunningGame
        {
            get => _runningGame;
            set => SetProperty(ref _runningGame, value);
        }

        private GameState _gameState = GameState.Stopped;
        public string GameStatusText
        {
            get
            {
                if (_gameState.Equals(GameState.Stopped) && _isStartPlanned) return "STARTING...";
                if (_gameState.Equals(GameState.Started) && _isStopPlanned) return "STOPPING...";
                if (_gameState.Equals(GameState.Started)) return "RUNNING";
                return string.Empty;
            }
        }

        public bool IsCloseGameEnabled
        {
            get
            {
                if (_gameState.Equals(GameState.Started) && !_isStopPlanned) return true;
                return false;
            }
        }
        #endregion

        #region Commands
        public Command CloseGameCommand { get; }
        private void CloseGameDelegate()
        {
            // OFDO: CloseGameDelegate
        }
        #endregion

        #region Constructor
        private GameRunningViewModel() : base("Game Running", true, false)
        {
            CloseGameCommand = new Command(this, target => ((GameRunningViewModel) target).CloseGameDelegate());

            Task.Run(WatchIsAnyGameRunning);
        }
        #endregion

        #region Methods PUBLIC
        public void PlanStart(FSimMan.Management.Game game)
        {
            _isStartPlanned = true;
            RunningGame = GameInfoCollection.Instance.GetGameInfo(game);
            MainViewModel.ViewModelSelector.OpenViewModel(this);
            // OFOD: PlanStart
        }

        //public void PlanStop()
        //{
        //    _isStopPlanned = true;
        //    // OFOD: PlanStop
        //}
        #endregion

        #region Methods PRIVATE
        private void UpdateProperties()
        {
            // OFDO: UpdateProperties
        }

        private void WatchIsAnyGameRunning()
        {
            ReadOnlyCollection<GameInfoBase> gameInfos = GameInfoCollection.Instance.GetAll();

            foreach (GameInfoBase gameInfo in gameInfos)
            {
                // OFOD: WatchIsAnyGameRunning
            }
        }
        #endregion
    }
}
