﻿using OF.Base.Client;
using OF.Base.Objects;
using OF.FSimMan.Management;
using OF.FSimMan.Utility;
using System.Diagnostics;

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
        private SettingsClient()
        {
            AppSettings.StoreTrigger += HandleAppSettingsStoreTrigger;
        }
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
                AppSettings.UpdateHandlers();
                if (doControlBusyIndicator) ResetBusyIndicator();
            }
        }
        #endregion

        #region Methods PRIVATE
        private void HandleAppSettingsStoreTrigger(object? sender, AppSettingsStoreTriggerEventArgs e)
        {
            StoreSettings();
        }

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
