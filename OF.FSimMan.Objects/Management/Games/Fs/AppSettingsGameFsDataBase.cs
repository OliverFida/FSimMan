using System.Xml.Serialization;

namespace OF.FSimMan.Management.Games.Fs
{
    public abstract class AppSettingsGameFsDataBase : AppSettingsGameDataBase
    {
        protected AppSettingsGameFsDataBase() { }
    }

    public abstract class AppSettingsGameFsDataBase<T> : AppSettingsGameDataBase<T> where T : AppSettingsGameFsBase
    {
        [XmlElement(IsNullable = false)]
        public string DataDirectoryPath = string.Empty;

        public override T FromData()
        {
            T temp = base.FromData();
            temp.DataDirectoryPath = DataDirectoryPath;
            return temp;
        }

        public override void ToData(T value)
        {
            base.ToData(value);
            DataDirectoryPath = value.DataDirectoryPath;
        }
    }
}
