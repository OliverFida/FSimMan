namespace OF.Base.Objects
{
    public abstract class EditableObject : BindingObject, IEditableObject
    {
        private Dictionary<string, object> _originalNonGenerics = new Dictionary<string, object>();
        private Dictionary<string, object> _originalObjects = new Dictionary<string, object>();
        private Dictionary<string, object> _originalCollections = new Dictionary<string, object>();

        private bool _isEditing = false;
        public bool IsEditing { get => _isEditing; }

        public bool IsModified
        {
            get
            {
                // Non-Generics
                foreach (var item in _originalNonGenerics)
                {
                    var prop = GetType().GetProperty(item.Key);
                    if (prop!.GetValue(this) == null && item.Value != null) return true;
                    if (prop!.GetValue(this) != null && item.Value == null) return true;
                    if (prop!.GetValue(this) == null) continue;
                    if (!prop!.GetValue(this)!.Equals(item.Value)) return true;
                }

                // Objects
                foreach (var item in _originalObjects)
                {
                    var prop = GetType().GetProperty(item.Key);
                    if (prop!.GetValue(this) == null && item.Value != null) return true;
                    if (prop!.GetValue(this) != null && item.Value == null) return true;
                    if (prop!.GetValue(this) == null) continue;
                    if (!prop!.GetValue(this)!.Equals(_originalObjects[item.Key])) return true;
                    if (((EditableObject)prop!.GetValue(this)!).IsModified) return true;
                }

                // IEditableCollection
                foreach (var item in _originalCollections)
                {
                    var prop = GetType().GetProperty(item.Key);
                    if (!prop!.GetValue(this)!.Equals(_originalCollections[item.Key])) return true;
                    if (((IEditableCollection)prop!.GetValue(this)!).IsModified) return true;
                }

                return false;
            }
        }

        public void BeginEdit()
        {
            if (_isEditing) return;

            // Non-Generics
            _originalNonGenerics.Clear();
            foreach (var prop in GetType().GetProperties()
                .Where(p => !p.GetType().IsGenericType))
            {
                if (!prop.CanRead || !prop.CanWrite) continue;

                _originalNonGenerics[prop.Name] = prop.GetValue(this)!;
            }

            // Objects
            _originalObjects.Clear();
            foreach (var prop in GetType().GetProperties()
                .Where(p => typeof(EditableObject).IsAssignableFrom(p.PropertyType) && p.PropertyType.IsGenericType == false && p.PropertyType != GetType()))
            {
                if (!prop.CanRead || !prop.CanWrite) continue;

                _originalObjects[prop.Name] = prop.GetValue(this)!;
                ((EditableObject)prop.GetValue(this)!).BeginEdit();
            }

            // IEditableCollection
            _originalCollections.Clear();
            foreach (var prop in GetType().GetProperties()
                .Where(p => typeof(IEditableCollection).IsAssignableFrom(p.PropertyType)))
            {
                if (!prop.CanRead || !prop.CanWrite) continue;

                _originalCollections[prop.Name] = prop.GetValue(this)!;
                ((IEditableCollection)prop.GetValue(this)!).BeginEdit();
            }

            _isEditing = true;
        }

        public void CancelEdit()
        {
            if (!_isEditing) return;

            // Non-Generics
            foreach (var orgVal in _originalNonGenerics)
            {
                var prop = GetType().GetProperty(orgVal.Key);
                if (prop == null) continue;
                if (!prop.CanWrite) continue;

                prop.SetValue(this, orgVal.Value);
            }
            _originalNonGenerics.Clear();

            // Objects
            foreach (var orgVal in _originalObjects)
            {
                var prop = GetType().GetProperty(orgVal.Key);
                if (prop == null) continue;
                if (!prop.CanWrite) continue;

                prop.SetValue(this, orgVal.Value);
                ((EditableObject)prop.GetValue(this)!).CancelEdit();
            }
            _originalObjects.Clear();

            // IEditableCollection
            foreach (var orgVal in _originalCollections)
            {
                var prop = GetType().GetProperty(orgVal.Key);
                if (prop == null) continue;
                if (!prop.CanWrite) continue;

                prop.SetValue(this, orgVal.Value);
                ((IEditableCollection)prop.GetValue(this)!).CancelEdit();
            }
            _originalCollections.Clear();

            _isEditing = false;
        }

        public void EndEdit()
        {
            if (!_isEditing) return;

            // Non-Generics
            _originalNonGenerics.Clear();

            // Objects
            foreach (var orgVal in _originalObjects)
            {
                var prop = GetType().GetProperty(orgVal.Key);
                if (prop == null) continue;
                if (!prop.CanWrite) continue;

                ((EditableObject)prop.GetValue(this)!).EndEdit();
            }
            _originalObjects.Clear();

            // IEditableCollection
            foreach (var orgVal in _originalCollections)
            {
                var prop = GetType().GetProperty(orgVal.Key);
                if (prop == null) continue;
                if (!prop.CanWrite) continue;

                ((IEditableCollection)prop.GetValue(this)!).EndEdit();
            }
            _originalCollections.Clear();

            _isEditing = false;
        }
    }
}
