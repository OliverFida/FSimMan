using OliverFida.Base;

namespace OliverFida.FSimMan.Config.ModPack
{
    public class ModPacks : EditableObjectBase
    {
        internal List<ModPack> _list = new List<ModPack>();
        public List<ModPack> List
        {
            get => (from p in _list orderby p.Title ascending select p).ToList();
        }

        public void AddModPack(ModPack modPack)
        {
            _list.Add(modPack);
            OnPropertyChanged(nameof(List));
        }

        public void RemoveModPack(ModPack modPack)
        {
            _list.Remove(modPack);
            OnPropertyChanged(nameof(List));
        }
    }
}
