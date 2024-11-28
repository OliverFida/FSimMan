using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Game;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public class Fs22ViewModel : FsViewModelBase
    {
        #region Commands
        protected override void NewModPackDelegate()
        {
            try
            {
                ModPack? modPack = new ModPack(Management.Game.FarmingSim22);

                Fs22EditModPackViewModel editModPackViewModel = new Fs22EditModPackViewModel(Management.EditMode.New);
                MainViewModel.ViewModelSelector.OpenViewModel(editModPackViewModel);
            }
            catch (Exception ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public Fs22ViewModel() : base(new Fs22Client()) { }
        #endregion
    }
}
