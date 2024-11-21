namespace OF.Base.Objects
{
    public abstract class DataObject : IDataObject
    {
        protected DataObject() { }
    }

    public abstract class DataObject<T> : DataObject, IDataObject<T>
    {
        public DataObject() { }

        public abstract T FromData();

        public abstract void ToData(T value);
    }
}
