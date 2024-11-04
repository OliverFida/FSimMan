namespace OliverFida.Base
{
    public abstract class ParentedDataObjectBase
    {
        protected ParentedDataObjectBase() { }
    }

    public abstract class ParentedDataObjectBase<T> : DataObjectBase
    {
        public ParentedDataObjectBase() { }

        public abstract T FromData(object parent);
        public abstract void ToData(T value);
    }
}
