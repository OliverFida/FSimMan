using OF.Base.Objects;

namespace OF.FSimMan.Management
{
    public class AppSettings : BindingObject
    {
        #region Application
        internal ApplicationMode _applicationMode = ApplicationMode.User;
        public ApplicationMode ApplicationMode
        {
            get => _applicationMode;
            set { if (SetProperty(ref _applicationMode, value)) OnTriggerStoreEvent(); }
        }

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

        #region FarmingSim 22
        internal bool _isFs22Active = false;
        public bool IsFs22Active
        {
            get => _isFs22Active;
            set { if (SetProperty(ref _isFs22Active, value)) UpdateVisiblility(); }
        }

        internal string _fs22GamePath = string.Empty;
        public string Fs22GamePath
        {
            get => _fs22GamePath;
            set
            {
                if (SetProperty(ref _fs22GamePath, value)) UpdateVisiblility();
            }
        }

        internal string _fs22DataPath = string.Empty;
        public string Fs22DataPath
        {
            get => _fs22DataPath;
            set
            {
                if (SetProperty(ref _fs22DataPath, value)) UpdateVisiblility();
            }
        }

        public bool IsFs22Visible
        {
            get => IsFs22Active && !string.IsNullOrWhiteSpace(Fs22GamePath) && !string.IsNullOrWhiteSpace(Fs22DataPath);
        }
        #endregion

        #region FarmingSim 25
        internal bool _isFs25Active = false;
        public bool IsFs25Active
        {
            get => _isFs25Active;
            set
            { if (SetProperty(ref _isFs25Active, value)) UpdateVisiblility(); }
        }

        internal string _fs25GamePath = string.Empty;
        public string Fs25GamePath
        {
            get => _fs25GamePath;
            set
            {
                if (SetProperty(ref _fs25GamePath, value)) UpdateVisiblility();
            }
        }

        internal string _fs25DataPath = string.Empty;
        public string Fs25DataPath
        {
            get => _fs25DataPath;
            set
            {
                if (SetProperty(ref _fs25DataPath, value)) UpdateVisiblility();
            }
        }

        public bool IsFs25Visible
        {
            get => IsFs25Active && !string.IsNullOrWhiteSpace(Fs25GamePath) && !string.IsNullOrWhiteSpace(Fs25DataPath);
        }
        #endregion

        #region Events
        public event EventHandler? TriggerStoreEvent;
        #endregion

        #region Methods PRIVATE
        private void UpdateVisiblility()
        {
            OnPropertyChanged(nameof(IsFs22Visible));
            OnPropertyChanged(nameof(IsFs25Visible));
            OnTriggerStoreEvent();
        }

        private void OnTriggerStoreEvent()
        {
            TriggerStoreEvent?.Invoke(this, EventArgs.Empty);
        }
        #endregion
    }
}
