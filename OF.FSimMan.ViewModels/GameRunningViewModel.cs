using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Management;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;

namespace OF.FSimMan.ViewModel
{
    public class GameRunningViewModel : ViewModelBase, ISingleton<GameRunningViewModel>
    {
        #region ISingleton
        private static readonly GameRunningViewModel _instance = new GameRunningViewModel();
        public static GameRunningViewModel Instance { get => _instance; }
        #endregion

        #region Properties
        private bool _isStartPlanned = false;
        private bool IsStartPlanned
        {
            get => _isStartPlanned;
            set { if (SetProperty(ref _isStartPlanned, value)) UpdateProperties(); }
        }

        private bool _isStopPlanned = false;
        private bool IsStopPlanned
        {
            get => _isStopPlanned;
            set { if (SetProperty(ref _isStopPlanned, value)) UpdateProperties(); }
        }

        private GameInfoBase? _runningGame;
        public GameInfoBase? RunningGame
        {
            get => _runningGame;
            private set { if (SetProperty(ref _runningGame, value)) UpdateProperties(); }
        }

        public string GameStatusText
        {
            get
            {
                if (RunningGame is null) return string.Empty;
                if (IsStartPlanned) return "STARTING...";
                if (IsStopPlanned) return "STOPPING...";
                return "RUNNING";
            }
        }

        public bool IsCloseGameEnabled
        {
            get
            {
                if (RunningGame is not null && !IsStartPlanned && !IsStopPlanned) return true;
                return false;
            }
        }
        #endregion

        #region Commands
        public Command CloseGameCommand { get; }
        private void CloseGameDelegate()
        {
            // OFDOI: CloseGameDelegate
        }
        #endregion

        #region Constructor
        private GameRunningViewModel() : base("Game Running", false, false)
        {
            CloseGameCommand = new Command(this, target => ((GameRunningViewModel)target).CloseGameDelegate());

            Task.Run(WatchIsAnyGameRunning);
        }
        #endregion

        #region Methods PUBLIC
        public void PlanStart(FSimMan.Management.Game game)
        {
            IsStartPlanned = true;

            RunningGame = GameInfoCollection.Instance.GetGameInfo(game);
            MainViewModel.ViewModelSelector.OpenViewModel(this);

            WatchRunningGame();
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
            OnPropertyChanged(nameof(GameStatusText));
        }

        private void WatchIsAnyGameRunning()
        {
            ReadOnlyCollection<GameInfoBase> gameInfos = GameInfoCollection.Instance.GetAll();

            while (true)
            {
                Thread.Sleep(5000);

                if (RunningGame is null && !IsStartPlanned)
                {
                    // Look for any game
                    foreach (GameInfoBase gameInfo in gameInfos)
                    {
                        Debug.WriteLine("Checking game: " + gameInfo.Title);

                        if (Process.GetProcessesByName(gameInfo.ProcessName).Count() > 0)
                        {
                            // Running game found
                            RunningGame = gameInfo;
                            MainViewModel.ViewModelSelector.OpenViewModel(this);
                            WatchRunningGame();
                        }
                    }
                }
            }
        }

        private void WatchRunningGame()
        {
            while (true)
            {
                if (RunningGame is not null && IsStartPlanned)
                {
                    // Expexcted game start occurred
                    if (Process.GetProcessesByName(RunningGame.ProcessName).Count() > 0) IsStartPlanned = false;
                }
                else if (RunningGame is not null && !IsStartPlanned)
                {
                    if (Process.GetProcessesByName(RunningGame.ProcessName).Count() == 0)
                    {
                        // Unexpected game exit
                        RunningGame = null;
                        MainViewModel.ViewModelSelector.CloseCurrentViewModel(); // OFDOI: Exception when FS running on startup and then closed
                        break;
                    }
                }
                else if (RunningGame is null) break;

                Thread.Sleep(2000);
            }
        }
        #endregion
    }
}
