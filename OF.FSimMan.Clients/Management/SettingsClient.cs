using OF.Base.Client;
using OF.Base.Objects;
using OF.FSimMan.Database.Context;
using OF.FSimMan.Management;

namespace OF.FSimMan.Client.Management
{
    public class SettingsClient : ClientBase, ISingleton<SettingsClient>
    {
        #region Properties
        private AppSettings? _appSettings = null;
        public AppSettings AppSettings
        {
            get
            {
                if (_appSettings is null) ReadSettings();
                return _appSettings!;
            }
        }
        #endregion

        #region Constructor
        private SettingsClient()
        {
            UpdateHandlers();
        }
        #endregion

        #region Methods PUBLIC
        public void StoreSettings(bool doControlBusyIndicator = true)
        {
            try
            {
                if (doControlBusyIndicator) IsBusy = true;

                using(SettingsDbContext db = new SettingsDbContext())
                {
                    _appSettings = db.StoreAppSettings(AppSettings);
                }
            }
            finally
            {
                UpdateHandlers();
                AppSettings.UpdateHandlers();
                if (doControlBusyIndicator) ResetBusyIndicator();
            }
        }
        #endregion

        #region Methods PRIVATE
        private void UpdateHandlers()
        {
            AppSettings.StoreTrigger -= HandleAppSettingsStoreTrigger;
            AppSettings.StoreTrigger += HandleAppSettingsStoreTrigger;
        }

        private void HandleAppSettingsStoreTrigger(object? sender, AppSettingsStoreTriggerEventArgs e)
        {
            StoreSettings();
        }

        private void ReadSettings()
        {
            try
            {
                IsBusy = true;

                using(SettingsDbContext db = new SettingsDbContext())
                {
                    AppSettings appSettings = db.ReadAppSettings();
                    _appSettings = appSettings;
                }
            }
            finally
            {
                AppSettings.UpdateHandlers();
                ResetBusyIndicator();
            }
        }
        #endregion

        #region ISingleton
        private static readonly SettingsClient _instance = new SettingsClient();
        public static SettingsClient Instance => _instance;
        #endregion
    }
}
