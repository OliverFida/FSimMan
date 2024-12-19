namespace OF.FSimMan.Management.Games.Fs
{
    public abstract class AppSettingsGameFsBase : AppSettingsGameBase
    {
        #region Properties

        internal string _dataDirectoryPath = string.Empty;
        public string DataDirectoryPath
        {
            get => _dataDirectoryPath;
            set { if (SetProperty(ref _dataDirectoryPath, value)) InvokeSettingsChanged(); }
        }

        internal AppSettingsGameFsStartArguments _startArguments = new AppSettingsGameFsStartArguments();
        public AppSettingsGameFsStartArguments StartArguments
        {
            get => _startArguments;
        }

        public override bool IsFullyConfigured
        {
            get
            {
                if (string.IsNullOrEmpty(DataDirectoryPath)) return false;

                return base.IsFullyConfigured;
            }
        }
        #endregion

        #region Methods INTERNAL
        internal void UpdateHandlers()
        {
            StartArguments.StoreTrigger -= HandleStartArgumentsStoreTrigger;
            StartArguments.StoreTrigger += HandleStartArgumentsStoreTrigger;
        }
        #endregion

        #region Methods PRIVATE
        private void HandleStartArgumentsStoreTrigger(object? sender, EventArgs e) => InvokeSettingsChanged();
        #endregion
    }
}
