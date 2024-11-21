using OF.Base.ViewModel;

namespace OF.FSimMan.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        #region Properties
        public static string WindowTitle
        {
            get => "FSimMan"; // OFDO
        }
        #endregion

        #region Constructor
        public MainViewModel() : base() { }
        #endregion
    }
}
