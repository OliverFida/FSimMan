using OF.Base.Client;
using OF.Base.Objects;
using OF.FSimMan.Management;
using OF.FSimMan.Utility;

namespace OF.FSimMan.Client.Management
{
    public class SettingsClient : ClientBase, ISingleton<SettingsClient>
    {
        private const string _fileName = "appSettings.xml";

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
        private SettingsClient() { }
        #endregion

        #region Methods PUBLIC
        public void StoreSettings(bool doControlBusyIndicator = true)
        {
            try
            {
                if (doControlBusyIndicator) IsBusy = true;

                AppSettingsData data = new AppSettingsData();
                data.ToData(AppSettings);
                FileSerializationHelper.SerializeConfigFile(_fileName, data);
            }
            finally
            {
                if (doControlBusyIndicator) ResetBusyIndicator();
            }
        }
        #endregion

        #region Methods PRIVATE
        private void ReadSettings()
        {
            try
            {
                IsBusy = true;

                AppSettingsData data = FileSerializationHelper.DeserializeConfigFile<AppSettingsData>(_fileName);
                AppSettings temp = data.FromData();

                if (!ReleaseFeatures.ApplicationModeCreator) temp.ApplicationModeValues = temp.ApplicationModeValues.Where(x => !x.Equals(ApplicationMode.Creator)).ToList();

                _appSettings = temp;
                StoreSettings(false);
            }
            finally
            {
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
