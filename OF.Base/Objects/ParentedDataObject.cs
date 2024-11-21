namespace OF.Base.Objects
{
    public abstract class ParentedDataObject<T> : DataObject<T>, IParentedDataObject<T>
    {
        public ParentedDataObject() { }

        public override T FromData()
        {
            return FromData(null);
        }

        public abstract T FromData(object? parent);
    }
}
