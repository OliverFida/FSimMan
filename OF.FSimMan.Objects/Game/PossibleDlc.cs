using OF.Base.Objects;

namespace OF.FSimMan.Game
{
    public class PossibleDlc : BindingObject
    {
        #region Properties
        private KnownDlc _dlc;
        public KnownDlc Dlc
        {
            get => _dlc;
        }

        private bool _isRequired = false;
        public bool IsRequired
        {
            get => _isRequired;
            set => SetProperty(ref _isRequired, value);
        }
        #endregion

        #region Constructor
        public PossibleDlc(KnownDlc dlc)
        {
            _dlc = dlc;
        }
        #endregion
    }
}
