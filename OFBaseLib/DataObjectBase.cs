namespace OliverFida.Base
{
    public abstract class DataObjectBase
    {
        protected DataObjectBase() { }
    }

    public abstract class DataObjectBase<T> : DataObjectBase
    {
        public DataObjectBase() { }

        public abstract T FromData();
        public abstract void ToData(T value);
    }
}
