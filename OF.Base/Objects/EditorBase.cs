namespace OF.Base.Objects
{
    public abstract class EditorBase<T> : BindingObject, IEditor<T> where T : IEditableObject
    {
        #region Properties
        private readonly T _objectToEdit;
        public T ObjectToEdit => _objectToEdit;
        #endregion

        #region Constructor
        public EditorBase(T objectToEdit)
        {
            _objectToEdit = objectToEdit;
        }
        #endregion

        #region Methods PROTECTED
        protected virtual void BeginEdit()
        {
            _objectToEdit.BeginEdit();
        }

        protected virtual void CancelEdit()
        {
            _objectToEdit.CancelEdit();
        }

        protected virtual void EndEdit()
        {
            _objectToEdit.EndEdit();
        }
        #endregion
    }
}
