namespace OF.FSimMan.Management.Games.Fs
{
    public abstract class AppSettingsGameFsBase : AppSettingsGameBase
    {
        #region Properties
        internal AppSettingsGameFsStartArguments _startArguments = new AppSettingsGameFsStartArguments();
        public AppSettingsGameFsStartArguments StartArguments
        {
            get => _startArguments;
        }
        #endregion

        #region Constructor
        public AppSettingsGameFsBase(Game game) : base (game) { }
        #endregion

        #region Methods INTERNAL
        internal void UpdateHandlers()
        {
            StartArguments.StoreTrigger -= HandleStartArgumentsStoreTrigger;
            StartArguments.StoreTrigger += HandleStartArgumentsStoreTrigger;
        }
        #endregion

        #region Methods PRIVATE
        private void HandleStartArgumentsStoreTrigger(object? sender, AppSettingsStoreTriggerEventArgs e) => InvokeSettingsChanged(e);
        #endregion
    }
}
