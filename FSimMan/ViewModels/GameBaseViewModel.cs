using OF.Base.ViewModel;

namespace OliverFida.FSimMan.ViewModels
{
    public abstract class GameBaseViewModel : ViewModelBase
    {
        private SupportedGame _game;
        public SupportedGame Game { get => _game; }

        public GameBaseViewModel(SupportedGame game)
        {
            _game = game;
        }

        public GameBaseViewModel(FsEdition fsEdition)
        {
            switch (fsEdition)
            {
                case FsEdition.Fs22:
                    _game = SupportedGame.Fs22;
                    break;
                case FsEdition.Fs25:
                    _game = SupportedGame.Fs25;
                    break;
            }
        }
    }
}
