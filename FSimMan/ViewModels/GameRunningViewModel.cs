using OliverFida.Base;
using System.Diagnostics;

namespace OliverFida.FSimMan.ViewModels
{
    public class GameRunningViewModel : ViewModelBase
    {
        #region Properties
        private SupportedGame _game;
        private string ProcessName
        {
            get
            {
                switch (_game)
                {
                    case SupportedGame.Fs22:
                        return "FarmingSimulator2022Game";
                    case SupportedGame.Fs25:
                        return "FarmingSimulator2025Game";
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        private bool _processRunning = false;
        public bool ProcessRunning
        {
            get => _processRunning;
            private set { if (SetProperty(ref _processRunning, value)) UpdateProperties(); }
        }

        private GameStatus _gameStatus = GameStatus.Started;
        public GameStatus GameStatus
        {
            get => _gameStatus;
            private set { if (SetProperty(ref _gameStatus, value)) UpdateProperties(); }
        }
        public string GameStatusText
        {
            get
            {
                if (_gameStatus == ViewModels.GameStatus.Started)
                {
                    if (ProcessRunning) return "GAME RUNNING";
                    return "GAME STARTING...";
                }
                return "GAME STOPPING...";
            }
        }

        public bool IsCloseGameEnabled
        {
            get
            {
                if (GameStatus == GameStatus.Started && !_processRunning) return false;
                if (GameStatus == GameStatus.Stopped && _processRunning) return false;

                return true;
            }
        }

        public string GameName
        {
            get
            {
                switch (_game)
                {
                    case SupportedGame.Fs22:
                        return "Farming Simulator 22";
                    case SupportedGame.Fs25:
                        return "Farming Simulator 25";
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public string GameImage
        {
            get
            {
                string path = "/UI/Resources/Images/";
                switch (_game)
                {
                    case SupportedGame.Fs22:
                        path += "FS22.png";
                        break;
                    case SupportedGame.Fs25:
                        path += "FS25.png";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                return path;
            }
        }
        #endregion

        #region Constructor
        public GameRunningViewModel(SupportedGame game)
        {
            _game = game;
            CloseGameCommand = new Command(this, target => ((GameRunningViewModel)target).CloseGame());

            WaitForGameLaunch();
            Task.Run(CheckIfGameClosed);
        }
        #endregion

        #region Commands
        public Command CloseGameCommand { get; }
        private async void CloseGame()
        {
            await Task.Run(() =>
            {
                GameStatus = GameStatus.Stopped;

                WaitForGameClosed();
            });
        }
        #endregion

        #region Methods PRIVATE
        private void UpdateProperties()
        {
            OnPropertyChanged(nameof(GameStatusText));
            OnPropertyChanged(nameof(IsCloseGameEnabled));
            OnPropertyChanged(nameof(GameName));
            OnPropertyChanged(nameof(GameImage));
        }

        private void WaitForGameLaunch()
        {
            Process[] processes;
            while (true)
            {
                processes = Process.GetProcessesByName(ProcessName);

                if (processes.Length > 0) break;

                Thread.Sleep(1000);
            }

            ProcessRunning = true;
        }

        private void WaitForGameClosed()
        {
            bool stopInitiated = false;
            int tries = 0;
            Process[] processes;
            while (true)
            {
                processes = Process.GetProcessesByName(ProcessName);

                if (processes.Length > 0 && !stopInitiated)
                {
                    processes[0].Close();
                    stopInitiated = true;
                    tries = 0;
                }

                if (processes.Length == 0) break;

                if (tries >= 5 && stopInitiated)
                {
                    processes[0].Kill();
                }

                tries++;
                Thread.Sleep(1000);
            }
        }

        private void CheckIfGameClosed()
        {
            Process[] processes;
            while (true)
            {
                processes = Process.GetProcessesByName(ProcessName);

                if (processes.Length == 0) break;

                Thread.Sleep(2000);
            }

            MainWindow.ViewModelSelector.CloseViewModel(this);
        }
        #endregion
    }
}
