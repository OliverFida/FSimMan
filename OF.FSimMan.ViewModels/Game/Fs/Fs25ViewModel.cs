using OF.FSimMan.Client.Game.Fs;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public class Fs25ViewModel : FsViewModelBase
    {
        #region Constructor
        public Fs25ViewModel() : base(new Fs25Client()) { }
        #endregion
    }
}
