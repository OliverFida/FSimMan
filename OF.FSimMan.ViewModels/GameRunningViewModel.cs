using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Game;
using OF.FSimMan.Management;
using OF.FSimMan.ViewModel.Game;

namespace OF.FSimMan.ViewModel
{
    public class GameRunningViewModel : ViewModelBase
    {
        #region Properties
        private readonly GameViewModelBase _gameViewModel;

        public string GameStatusText
        {
            get
            {
                if (((IGameClient)_gameViewModel.Client).GameState == GameState.Started)
                {
                    if (((IGameClient)_gameViewModel.Client).IsGameRunning) return "RUNNING";
                    return "STARTING...";
                }
                return "STOPPING...";
            }
        }

        public bool IsCloseGameEnabled
        {
            get
            {
                IGameClient client = (IGameClient)_gameViewModel.Client;
                if (client.GameState == GameState.Started && !client.IsGameRunning ||
                    client.GameState == GameState.Stopped && client.IsGameRunning) return false;

                return true;
            }
        }

        public string GameName
        {
            get
            {
                switch (((IGameClient)_gameViewModel.Client).Game)
                {
                    case FSimMan.Management.Game.FarmingSim22:
                        return "Farming Simulator 22";
                    case FSimMan.Management.Game.FarmingSim25:
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
                string path = "pack://application:,,,/OF.FSimMan.Resources;component/Logos/";
                switch (((IGameClient)_gameViewModel.Client).Game)
                {
                    case FSimMan.Management.Game.FarmingSim22:
                        path += "FS22.png";
                        break;
                    case FSimMan.Management.Game.FarmingSim25:
                        path += "FS25.png";
                        break;
                    default:
                        throw new NotImplementedException();
                }

                return path;
            }
        }
        #endregion

        #region Commands
        public Command CloseGameCommand { get; }
        private void CloseGameDelegate()
        {
            Task.Run(() =>
            {
                try
                {
                    IGameClient client = (IGameClient)_gameViewModel.Client;
                    client.StopGame();
                    MainViewModel.ViewModelSelector.CloseCurrentViewModel();
                }
                catch (OfException ex)
                {
                    UiFunctions.ShowError(ex);
                }
            });
        }
        #endregion

        #region Constructor
        public GameRunningViewModel(GameViewModelBase gameViewModel)
        {
            _gameViewModel = gameViewModel;

            CloseGameCommand = new Command(this, target => ((GameRunningViewModel)target).CloseGameDelegate());
            ((IGameClient)_gameViewModel.Client).GameStateChanged += HandleGameStateChanged;
            Task.Run(WatchIsGameClosed);
        }
        #endregion

        #region Methods PRIVATE
        private void HandleGameStateChanged(object? sender, EventArgs e)
        {
            OnPropertyChanged(nameof(GameStatusText));
            OnPropertyChanged(nameof(IsCloseGameEnabled));
            OnPropertyChanged(nameof(GameName));
            OnPropertyChanged(nameof(GameImage));
        }

        private void WatchIsGameClosed()
        {
            ((IGameClient)_gameViewModel.Client).WaitForGameState(GameState.SelfStopped, false);
            MainViewModel.ViewModelSelector.CloseCurrentViewModel();
        }
        #endregion
    }
}
