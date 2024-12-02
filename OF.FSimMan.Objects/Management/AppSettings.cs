using OF.FSimMan.Management.Games;

namespace OF.FSimMan.Management
{
    public class AppSettings : AppSettingsBase
    {
        #region Application
        internal ApplicationMode _applicationMode = ApplicationMode.User;
        public ApplicationMode ApplicationMode
        {
            get => _applicationMode;
            set { if (SetProperty(ref _applicationMode, value)) InvokeSettingsChanged(); }
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
            set { if (SetProperty(ref _lastSelectedView, value)) InvokeSettingsChanged(); }
        }
        #endregion

        #region Games
        internal readonly List<AppSettingsGameBase> _games = new List<AppSettingsGameBase>();
        public List<AppSettingsGameBase> Games => _games;

        public T GetGameSettings<T>() where T : AppSettingsGameBase
        {
            T? matchingSettings = Games.OfType<T>().SingleOrDefault();
            if (matchingSettings is not null) return matchingSettings;

            T temp = Activator.CreateInstance<T>();
            Games.Add(temp);
            return temp;
        }
        #endregion
    }
}
