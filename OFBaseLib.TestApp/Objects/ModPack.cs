using OliverFida.Base;

namespace OFBaseLib.TestApp.Objects
{
    internal class ModPack : EditableObjectBase
    {
        private bool _isSuperToll = false;
        public bool IsSuperToll
        {
            get => _isSuperToll;
            private set => _isSuperToll = value;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        private Author _author;
        public Author Author
        {
            get => _author;
            set => _author = value;
        }

        private EditableObservableCollection<Mod> _mods;
        public EditableObservableCollection<Mod> Mods
        {
            get => _mods;
            private set => _mods = value;
        }

        public ModPack(string name, Author author)
        {
            _name = name;
            _author = author;
            _mods = new EditableObservableCollection<Mod>();
        }
    }
}
