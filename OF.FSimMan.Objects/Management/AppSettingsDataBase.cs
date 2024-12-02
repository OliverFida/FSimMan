using OF.Base.Objects;
using System.Xml.Serialization;

namespace OF.FSimMan.Management
{
    public abstract class AppSettingsDataBase : DataObject
    {
        protected AppSettingsDataBase() { }
    }

    [XmlRoot("AppSettings")]
    public abstract class AppSettingsDataBase<T> : DataObject<T>
    {

    }
}
