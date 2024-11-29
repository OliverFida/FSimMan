using OF.Base.Objects;
using System.Xml.Serialization;

namespace OF.FSimMan.Management
{
    [XmlRoot("AppSettings")]
    public class AppSettingsData : DataObject<AppSettings>
    {
        // Application
        [XmlElement(IsNullable = false)]
        public ApplicationMode ApplicationMode = ApplicationMode.None;

        [XmlElement(IsNullable = false)]
        public string LastSelectedView = string.Empty;

        // FarmingSim 22
        public bool IsFs22Active = false;

        public string Fs22GamePath = string.Empty;

        public string Fs22DataPath = string.Empty;

        // FarmingSim 25
        public bool IsFs25Active = false;

        public string Fs25GamePath = string.Empty;

        public string Fs25DataPath = string.Empty;

        public override AppSettings FromData()
        {
            AppSettings temp = new AppSettings
            {
                _applicationMode = ApplicationMode,
                _lastSelectedView = LastSelectedView,
                _isFs22Active = IsFs22Active,
                _fs22GamePath = Fs22GamePath,
                _fs22DataPath = Fs22DataPath,
                _isFs25Active = IsFs25Active,
                _fs25GamePath = Fs25GamePath,
                _fs25DataPath = Fs25DataPath
            };

            if (temp._applicationMode == ApplicationMode.None) temp._applicationMode = ApplicationMode.User;

            return temp;
        }

        public override void ToData(AppSettings value)
        {
            ApplicationMode = value._applicationMode;
            LastSelectedView = value.LastSelectedView;
            IsFs22Active = value.IsFs22Active;
            Fs22GamePath = value.Fs22GamePath;
            Fs22DataPath = value.Fs22DataPath;
            IsFs25Active = value.IsFs25Active;
            Fs25GamePath = value.Fs25GamePath;
            Fs25DataPath = value.Fs25DataPath;
        }
    }
}
