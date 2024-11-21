namespace OF.Base.Objects
{
    public interface IDataObject
    {

    }

    public interface IDataObject<T> : IDataObject
    {
        public abstract T FromData();
        public abstract void ToData(T value);
    }
}
