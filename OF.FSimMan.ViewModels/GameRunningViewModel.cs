using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Management;
using System.Collections.ObjectModel;
using System.Diagnostics;

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
        //public Command CloseGameCommand { get; }
        //private void CloseGameDelegate()
        //{
        //    try
        //    {
        //        PlanStop();
        //        // OFDO: GameClient.StopGame();
        //    }
        //    catch(Exception ex)
        //    {
        //        UiFunctions.ShowError(ex);
        //    }
        //}
        #endregion

        #region Constructor
        private GameRunningViewModel() : base("Game Running")
        {
            //CloseGameCommand = new Command(this, target => ((GameRunningViewModel)target).CloseGameDelegate());

            Task.Run(WatchIsAnyGameRunning);
        }
        #endregion

        #region Methods PUBLIC
        public void PlanStart(FSimMan.Management.Game game)
        {
            IsStartPlanned = true;

            RunningGame = GameInfoCollection.Instance.GetGameInfo(game);
            Open();

            Task.Run(WatchRunningGame);
        }

        public void PlanStop()
        {
            IsStopPlanned = true;
        }
        #endregion

        #region Methods PRIVATE
        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(GameStatusText));
            OnPropertyChanged(nameof(IsCloseGameEnabled));
        }

        private void Open()
        {
            if (MainViewModel.ViewModelSelector.CurrentViewModel is not null) MainViewModel.ViewModelSelector.CurrentViewModel.PreventAutoclose = true;

            MainViewModel.ViewModelSelector.OpenViewModel(this);
        }

        private void Close()
        {
            MainViewModel.ViewModelSelector.CloseViewModel(this);

            if (MainViewModel.ViewModelSelector.CurrentViewModel is not null &&
                MainViewModel.ViewModelSelector.CurrentViewModel.PreventAutoclose) MainViewModel.ViewModelSelector.CurrentViewModel.PreventAutoclose = false;
        }

        private void WatchIsAnyGameRunning()
        {
            try
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
                            if (Process.GetProcessesByName(gameInfo.ProcessName).Count() > 0)
                            {
                                // Running game found
                                RunningGame = gameInfo;
                                Open();
                                WatchRunningGame();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                UiFunctions.ShowError(ex);
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
                        // Game exit
                        RunningGame = null;
                        Close();
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
