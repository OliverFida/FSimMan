using OliverFida.Base;

namespace OFBaseLib.TestApp.Objects
{
    internal class Mod : EditableObjectBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        public Mod(string name)
        {
            _name = name;
        }
    }
}
