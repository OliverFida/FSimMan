using CLS.Core.Objects;

namespace OF.FSimMan.Base
{
    public abstract class ParentedDataObjectBase<T, TParent> : DataObjectBase<T> where T : ObjectBase where TParent : ObjectBase
    {
        public ParentedDataObjectBase() { }

        public override T FromData()
        {
            throw new InvalidOperationException();
        }

        public abstract T FromData(TParent parent);
    }
}
