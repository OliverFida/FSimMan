using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Game;

namespace OF.FSimMan.ViewModel.Game.Fs
{
    public class Fs25ViewModel : FsViewModelBase
    {
        #region Commands
        protected override void NewModPackDelegate()
        {
            try
            {
                ModPack? modPack = new ModPack(Management.Game.FarmingSim25);

                Fs25EditModPackViewModel editModPackViewModel = new Fs25EditModPackViewModel(Management.EditMode.New);
                MainViewModel.ViewModelSelector.OpenViewModel(editModPackViewModel);
            }
            catch (Exception ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public Fs25ViewModel() : base(new Fs25Client()) { }
        #endregion
    }
}
