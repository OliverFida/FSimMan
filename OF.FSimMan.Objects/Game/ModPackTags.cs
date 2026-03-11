using CLS.Core.Objects.Editable;
using System.Collections.ObjectModel;

namespace OF.FSimMan.Game
{
    public class ModPackTags : EditableObjectBase
    {
        #region Properties

        internal ObservableCollection<ModPackTag> _tags = new ObservableCollection<ModPackTag>();
        public ObservableCollection<ModPackTag> Tags
        {
            get => _tags;
            private set
            {
                if (SetProperty(ref _tags, value)) OnPropertyChanged(nameof(IsGenerallyImported));
            }
        }

        public bool IsGenerallyImported
        {
            get => IsImported || IsImportedButEdited || IsImportedAsNew;
        }

        public bool IsImported
        {
            get => Tags.Contains(ModPackTag.Imported);
        }

        public bool IsImportedButEdited
        {
            get => Tags.Contains(ModPackTag.ImportedButEdited);
        }

        public bool IsImportedAsNew
        {
            get => Tags.Contains(ModPackTag.ImportedAsNew);
        }
        #endregion

        #region Methods PUBLIC
        public void Set(ModPackTag newTag)
        {
            switch (newTag)
            {
                case ModPackTag.Imported:
                    if (Tags.Contains(ModPackTag.ImportedButEdited)) Tags.Remove(ModPackTag.ImportedButEdited);
                    if (Tags.Contains(ModPackTag.ImportedAsNew)) Tags.Remove(ModPackTag.ImportedAsNew);
                    break;
                case ModPackTag.ImportedButEdited:
                    if (Tags.Contains(ModPackTag.Imported)) Tags.Remove(ModPackTag.Imported);
                    if (Tags.Contains(ModPackTag.ImportedAsNew)) Tags.Remove(ModPackTag.ImportedAsNew);
                    break;
                case ModPackTag.ImportedAsNew:
                    if (Tags.Contains(ModPackTag.Imported)) Tags.Remove(ModPackTag.Imported);
                    if (Tags.Contains(ModPackTag.ImportedButEdited)) Tags.Remove(ModPackTag.ImportedButEdited);
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (!Tags.Contains(newTag)) Tags.Add(newTag);
        }
        #endregion
    }
}
