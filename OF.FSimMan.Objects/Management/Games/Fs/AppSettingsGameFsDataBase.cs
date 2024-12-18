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

        [XmlElement(IsNullable = false)]
        public AppSettingsGameFsStartArgumentsData StartArguments = new AppSettingsGameFsStartArgumentsData();

        public override T FromData()
        {
            T temp = base.FromData();
            temp._dataDirectoryPath = DataDirectoryPath;
            temp._startArguments = StartArguments.FromData();
            return temp;
        }

        public override void ToData(T value)
        {
            base.ToData(value);
            DataDirectoryPath = value.DataDirectoryPath;
            AppSettingsGameFsStartArgumentsData startArguments = new AppSettingsGameFsStartArgumentsData();
            startArguments.ToData(value.StartArguments);
            StartArguments = startArguments;
        }
    }
}
