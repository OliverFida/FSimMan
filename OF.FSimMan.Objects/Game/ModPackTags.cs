using OF.Base.Objects;
using System.Collections.ObjectModel;

namespace OF.FSimMan.Game
{
    public class ModPackTags : EditableObject
    {
        #region Properties

        internal ObservableCollection<ModPackTag> _tags = new ObservableCollection<ModPackTag>();
        public ObservableCollection<ModPackTag> Tags
        {
            get => _tags;
            private set
            {
                if (SetProperty(ref _tags, value))
                {
                    OnPropertyChanged(nameof(IsImported));
                    OnPropertyChanged(nameof(IsImportedButEdited));
                }
            }
        }

        public bool IsImported
        {
            get => Tags.Contains(ModPackTag.Imported);
        }

        public bool IsImportedButEdited
        {
            get => Tags.Contains(ModPackTag.ImportedButEdited);
        }
        #endregion

        #region Methods PUBLIC
        public void Set(ModPackTag newTag)
        {
            switch (newTag)
            {
                case ModPackTag.Imported:
                    if (Tags.Contains(ModPackTag.ImportedButEdited)) Tags.Remove(ModPackTag.ImportedButEdited);
                    Tags.Add(ModPackTag.Imported);
                    break;
                case ModPackTag.ImportedButEdited:
                    if (Tags.Contains(ModPackTag.Imported)) Tags.Remove(ModPackTag.Imported);
                    Tags.Add(ModPackTag.ImportedButEdited);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
        #endregion
    }
}
