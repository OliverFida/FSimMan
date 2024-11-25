using OF.Base.Objects;

namespace OF.FSimMan.Management
{
    public class AppSettings : BindingObject
    {
        #region Application
        internal ApplicationMode _applicationMode = ApplicationMode.User;
        public bool IsApplicationModeCreator => _applicationMode == ApplicationMode.Creator;

        private List<ApplicationMode>? _applicationModeValues;
        public List<ApplicationMode> ApplicationModeValues
        {
            get
            {
                if (_applicationModeValues == null) _applicationModeValues = Enum.GetValues<ApplicationMode>().Where(x => x != ApplicationMode.None).ToList();
                return _applicationModeValues;
            }
            set => SetProperty(ref _applicationModeValues, value);
        }
        #endregion

        // OFDO
    }
}
