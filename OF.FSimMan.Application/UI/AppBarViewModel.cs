using OF.Base.Objects;
using OF.Base.ViewModel;
using OF.FSimMan.ViewModel;

namespace OF.FSimMan.UI
{
    public class AppBarViewModel : ViewModelBase
    {
        #region Properties
        // OFDO: Properties
        #endregion

        #region Commands
        public Command UpdateCommand { get; }
        private async Task UpdateDelegate()
        {
            await MainViewModel.ExecuteUpdate();
        }

        // OFDO: Commands
        #endregion

        #region Constructor
        public AppBarViewModel()
        {
            UpdateCommand = new Command(this, async target => await ((AppBarViewModel)target).UpdateDelegate());
        }
        #endregion

        #region Methods PRIVATE
        // OFDO: Handler
        #endregion
    }
}
