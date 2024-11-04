using System.Collections.ObjectModel;

namespace OliverFida.Base
{
    public class EditableObservableCollection<T> : ObservableCollection<T>, IEditableCollection where T : EditableObjectBase
    {
        private List<T> _originalValues = new List<T>();

        private bool _isEditing = false;
        public bool IsEditing { get => _isEditing; }

        public bool IsModified
        {
            get
            {
                if (_originalValues.Count != Count) return true;

                var items = GetEnumerator();
                var orgItems = _originalValues.GetEnumerator();

                while (items.MoveNext() && orgItems.MoveNext())
                {
                    if (ReferenceEquals(items.Current, orgItems.Current) == false) return true;
                    if (items.Current.IsModified) return true;
                }

                return false;
            }
        }

        public void BeginEdit()
        {
            if (_isEditing) return;

            foreach (var item in this)
            {
                _originalValues.Add(item);
                item.BeginEdit();
            }

            _isEditing = true;
        }

        public void CancelEdit()
        {
            if (!IsEditing) return;

            Clear();
            foreach (var item in _originalValues)
            {
                Add(item);
                item.CancelEdit();
            }
            _originalValues.Clear();

            _isEditing = false;
        }

        public void EndEdit()
        {
            if (!_isEditing) return;

            _originalValues.Clear();

            _isEditing = false;
        }
    }
}
