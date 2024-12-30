using OF.Base.Objects;

namespace OF.FSimMan.Management
{
    public abstract class AppSettingsDataBase<T> : DataObject<T>
    {
        public int Id { get; set; }
    }
}
