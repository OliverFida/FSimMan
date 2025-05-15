using OF.Base.Objects;
using OF.FSimMan.Management.Exceptions;
using OF.FSimMan.Management.Games;
using OF.FSimMan.Management.Games.Fs;
using OF.FSimMan.Management.Modi;

namespace OF.FSimMan.Management
{
    public class AppSettings : AppSettingsBase
    {
        #region Application
        internal ApplicationMode _applicationMode = ApplicationMode.User;
        public ApplicationMode ApplicationMode
        {
            get => _applicationMode;
            set => SetProperty(ref _applicationMode, value);
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

        internal string _lastSelectedView = string.Empty;
        public string LastSelectedView
        {
            get => _lastSelectedView;
            set => SetProperty(ref _lastSelectedView, value);
        }

        internal string _lastVersionChangelogDisplayed = string.Empty;
        public string LastVersionChangelogDisplayed
        {
            get => _lastVersionChangelogDisplayed;
            set => SetProperty(ref _lastVersionChangelogDisplayed, value);
        }

        internal string _modificationKey = string.Empty;
        public string ModificationKey
        {
            get => _modificationKey;
            set => SetProperty(ref _modificationKey, value);
        }

        internal bool _isModiKeyValid = false;
        public bool IsModiKeyValid
        {
            get => _isModiKeyValid;
            private set => SetProperty(ref _isModiKeyValid, value);
        }

        public ModificationKey SelectedModiKey
        {
            get
            {
                switch (ModificationKey)
                {
                    case "2025!E83.Sync":
                        return Modi.ModificationKey.E83;
                    default:
                        return Modi.ModificationKey.None;
                }
            }
        }
        #endregion

        #region Games
        internal readonly List<GameSettingsBase> _games = new List<GameSettingsBase>();
        public List<GameSettingsBase> Games => _games;

        public T GetGameSettings<T>() where T : GameSettingsBase
        {
            T? matchingSettings = Games.OfType<T>().SingleOrDefault();
            if (matchingSettings is not null) return matchingSettings;

            T temp = Activator.CreateInstance<T>();
            Games.Add(temp);
            return temp;
        }

        public GameSettingsBase GetGameSettings(Game game)
        {
            switch (game)
            {
                case Game.FarmingSim22:
                    return GetGameSettings<GameSettingsFs22>();
                case Game.FarmingSim25:
                    return GetGameSettings<GameSettingsFs25>();
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion

        #region Events
        public event EventHandler<AppSettingsModPackAutogenerationNowPossibleEventArgs>? ModPackAutogenerationNowPossible;
        #endregion

        #region Methods PUBLIC
        public void UpdateHandlers()
        {
            foreach (var game in Games)
            {
                game.StoreTrigger -= HandleGameStoreTrigger;
                game.StoreTrigger += HandleGameStoreTrigger;

                game.ModPackAutogenerationNowPossible -= HandleGameModPackAutogenerationNowPossible; ;
                game.ModPackAutogenerationNowPossible += HandleGameModPackAutogenerationNowPossible; ;

                if (game.GetType().IsAssignableTo(typeof(GameSettingsBase))) ((GameSettingsBase)game).UpdateHandlers();
            }
        }

        public void CheckModiKey()
        {
            try
            {
                OnPropertyChanged(nameof(SelectedModiKey));
                if (SelectedModiKey.Equals(Modi.ModificationKey.None))
                    throw new UnknownModiKeyException();

                IsModiKeyValid = true;
            }
            catch (OfException)
            {
                IsModiKeyValid = false;
                throw;
            }
        }
        #endregion

        #region Methods PRIVATE
        private void HandleGameStoreTrigger(object? sender, AppSettingsStoreTriggerEventArgs e) => InvokeSettingsChanged(e);

        private void HandleGameModPackAutogenerationNowPossible(object? sender, AppSettingsModPackAutogenerationNowPossibleEventArgs e) => ModPackAutogenerationNowPossible?.Invoke(sender, e);
        #endregion
    }
}
