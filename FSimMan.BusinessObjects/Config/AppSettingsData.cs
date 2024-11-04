using OliverFida.Base;
using System.Xml.Serialization;

namespace OliverFida.FSimMan.Config
{
    [XmlRoot("AppSettings")]
    public class AppSettingsData : DataObjectBase<AppSettings>
    {
        public ApplicationMode ApplicationMode;

        public bool IsFs22Active;

        [XmlElement(IsNullable = false)]
        public string Fs22GamePath = string.Empty;

        [XmlElement(IsNullable = false)]
        public string Fs22DataPath = string.Empty;

        public bool IsFs25Active;

        [XmlElement(IsNullable = false)]
        public string Fs25GamePath = string.Empty;

        [XmlElement(IsNullable = false)]
        public string Fs25DataPath = string.Empty;


        public override AppSettings FromData()
        {
            AppSettings temp = new AppSettings()
            {
                _applicationMode = ApplicationMode,
                _isFs22Active = IsFs22Active,
                _fs22GamePath = Fs22GamePath,
                _fs22DataPath = Fs22DataPath,
                _isFs25Active = IsFs25Active,
                _fs25GamePath = Fs25GamePath,
                _fs25DataPath = Fs25DataPath
            };

            if (temp.ApplicationMode == ApplicationMode.None) temp._applicationMode = ApplicationMode.User;

            return temp;
        }

        public override void ToData(AppSettings value)
        {
            ApplicationMode = value.ApplicationMode;
            IsFs22Active = value.IsFs22Active;
            Fs22GamePath = value.Fs22GamePath;
            Fs22DataPath = value.Fs22DataPath;
            IsFs25Active = value.IsFs25Active;
            Fs25GamePath = value.Fs25GamePath;
            Fs25DataPath = value.Fs25DataPath;
        }
    }
}
