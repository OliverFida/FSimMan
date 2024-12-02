using OF.Base.Objects;
using OF.Base.Wpf.UiFunctions;
using OF.FSimMan.Client.Game;
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
                ModPack modPack = ((IGameClient)Client).GetNewModPack();
                _editModPackViewModel = new Fs22EditModPackViewModel(Management.EditMode.New, modPack, (Fs22Client)Client);

                _editModPackViewModel.ViewModelClosedEvent += HandleEditModPackViewModelClosedEvent;
                MainViewModel.ViewModelSelector.OpenViewModel(_editModPackViewModel);
            }
            catch (Exception ex)
            {
                UiFunctions.ShowError(ex);
            }
        }

        protected override void EditModpackDelegate()
        {
            try
            {
                if (EditModpackCommand.Parameter == null) return;
                ModPack modPack = (ModPack)EditModpackCommand.Parameter;

                _editModPackViewModel = new Fs22EditModPackViewModel(Management.EditMode.Edit, modPack, (Fs22Client)Client);

                _editModPackViewModel.ViewModelClosedEvent += HandleEditModPackViewModelClosedEvent;
                MainViewModel.ViewModelSelector.OpenViewModel(_editModPackViewModel);
            }
            catch (OfException ex)
            {
                UiFunctions.ShowError(ex);
            }
        }
        #endregion

        #region Constructor
        public Fs22ViewModel() : base(new Fs22Client()) { }
        #endregion

        #region Methods PRIVATE
        private void HandleEditModPackViewModelClosedEvent(object? sender, EventArgs e)
        {
            ((Fs22Client)Client).RefreshModPacks();
            _editModPackViewModel!.ViewModelClosedEvent -= HandleEditModPackViewModelClosedEvent;
        }
        #endregion
    }
}
