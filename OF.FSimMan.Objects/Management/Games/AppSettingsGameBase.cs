namespace OF.FSimMan.Management.Games
{
    public abstract class AppSettingsGameBase : AppSettingsBase
    {
        #region Properties
        internal bool _isEnabled = false;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { if (SetProperty(ref _isEnabled, value)) InvokeSettingsChanged(); }
        }

        internal string _exeDirectoryPath = string.Empty;
        public string ExeDirectoryPath
        {
            get => _exeDirectoryPath;
            set { if (SetProperty(ref _exeDirectoryPath, value)) InvokeSettingsChanged(); }
        }

        public virtual bool IsFullyConfigured
        {
            get
            {
                if (!IsEnabled) return false;
                if (string.IsNullOrEmpty(ExeDirectoryPath)) return false;

                return true;
            }
        }
        #endregion

        #region Methods PROTECTED
        protected override void InvokeSettingsChanged()
        {
            OnPropertyChanged(nameof(IsFullyConfigured));
            base.InvokeSettingsChanged();
        }
        #endregion
    }
}
