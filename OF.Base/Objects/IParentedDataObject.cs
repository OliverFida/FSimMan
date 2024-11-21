namespace OF.Base.Objects
{
    public interface IParentedDataObject<T> : IDataObject<T>
    {
        public abstract T FromData(object parent);
    }
}
