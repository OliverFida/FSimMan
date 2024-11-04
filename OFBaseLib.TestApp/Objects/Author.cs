using OliverFida.Base;

namespace OFBaseLib.TestApp.Objects
{
    internal class Author : EditableObjectBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public Author(string name)
        {
            _name = name;
        }
    }
}
