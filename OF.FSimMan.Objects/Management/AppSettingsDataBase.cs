using OF.Base.Objects;
using System.Xml.Serialization;

namespace OF.FSimMan.Management
{
    public abstract class AppSettingsDataBase : DataObject
    {
        protected AppSettingsDataBase() { }
    }

    public abstract class AppSettingsDataBase<T> : DataObject<T>
    {
        [XmlElement(IsNullable = false)]
        public string ApplicationVersion = CurrentApplication.AssemblyVersionText.Replace("v", "");
    }
}
