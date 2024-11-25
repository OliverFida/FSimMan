using OF.FSimMan.Client.Game.Fs;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public class Fs22ViewModel : FsViewModelBase
    {
        #region Constructor
        public Fs22ViewModel() : base(new Fs22Client()) { }
        #endregion
    }
}
