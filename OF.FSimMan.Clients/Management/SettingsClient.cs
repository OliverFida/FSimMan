using OF.Base.Client;
using OF.Base.Objects;
using OF.FSimMan.Client.Game;
using OF.FSimMan.Client.Game.Fs;
using OF.FSimMan.Database.Access;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games;
using OF.FSimMan.Management.Games.Fs;

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

                AppSettings temp = SettingsDbAccess.Instance.StoreAppSettings(AppSettings);
                if (AppSettings.Id.Equals(0)) AppSettings.Id = temp.Id;
            }
            finally
            {
                OnPropertyChanged(nameof(AppSettings));
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

        private async void HandleAppSettingsStoreTrigger(object? sender, AppSettingsStoreTriggerEventArgs e)
        {
            if (e.Type.IsAssignableTo(typeof(GameSettingsBase))) await CheckIfGameGettingDisabled(e);
            StoreSettings();
        }

        private void ReadSettings()
        {
            try
            {
                IsBusy = true;

                _appSettings = SettingsDbAccess.Instance.ReadAppSettings();
            }
            finally
            {
                AppSettings.UpdateHandlers();
                ResetBusyIndicator();
            }
        }

        private async Task CheckIfGameGettingDisabled(AppSettingsStoreTriggerEventArgs e)
        {
            if (e.PropertyName is null || !e.PropertyName.Equals(nameof(GameSettingsBase.IsEnabled))) return;

            GameSettingsBase gameSettings;
            GameClientBase gameClient;

            switch (e.Type.Name)
            {
                case nameof(GameSettingsFs22):
                    gameSettings = AppSettings.GetGameSettings<GameSettingsFs22>();
                    gameClient = new Fs22Client(false);
                    break;
                case nameof(GameSettingsFs25):
                    gameSettings = AppSettings.GetGameSettings<GameSettingsFs25>();
                    gameClient = new Fs25Client(false);
                    break;
                default:
                    throw new NotImplementedException();
            }
            if (gameSettings.IsEnabled) return;

            await gameClient.InitializeAsync();
            gameClient.ResetGameModFolder();
        }
        #endregion

        #region ISingleton
        private static readonly SettingsClient _instance = new SettingsClient();
        public static SettingsClient Instance => _instance;
        #endregion
    }
}
