using OF.Base.Objects;
using System.Xml.Serialization;

namespace OF.FSimMan.Management.Games
{
    public abstract class AppSettingsGameDataBase : DataObject
    {
        protected AppSettingsGameDataBase() { }
    }

    public abstract class AppSettingsGameDataBase<T> : AppSettingsGameDataBase, IDataObject<T> where T : AppSettingsGameBase
    {
        [XmlElement(IsNullable = false)]
        public bool IsEnabled = false;

        [XmlElement(IsNullable = false)]
        public string ExeDirectoryPath = string.Empty;

        public virtual T FromData()
        {
            T temp = Activator.CreateInstance<T>();
            temp.IsEnabled = IsEnabled;
            temp.ExeDirectoryPath = ExeDirectoryPath;
            return temp;
        }

        public virtual void ToData(T value)
        {
            IsEnabled = value.IsEnabled;
            ExeDirectoryPath = value.ExeDirectoryPath;
        }
    }
}
