using OF.Base.Objects;
using System.Xml.Serialization;

namespace OF.FSimMan.Management
{
    public class AppSettingsData : DataObject<AppSettings>
    {
        [XmlElement(IsNullable = false)]
        public ApplicationMode ApplicationMode;

        // OFDO

        public override AppSettings FromData()
        {
            AppSettings temp = new AppSettings
            {
                _applicationMode = ApplicationMode
            };

            if (temp._applicationMode == ApplicationMode.None) temp._applicationMode = ApplicationMode.User;

            return temp;
        }

        public override void ToData(AppSettings value)
        {
            ApplicationMode = value._applicationMode;
        }
    }
}
