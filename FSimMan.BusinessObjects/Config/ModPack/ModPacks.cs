using OF.Base.Objects;
using OliverFida.FSimMan.Exceptions;

namespace OliverFida.FSimMan.Config.ModPack
{
    public class ModPacks : EditableObject
    {
        internal List<ModPack> _list = new List<ModPack>();
        public List<ModPack> List
        {
            get => (from p in _list orderby p.Title ascending select p).ToList();
        }

        public void AddModPack(ModPack modPack)
        {
            ModPack? existingModPack = (from p in _list where p.Key.Equals(modPack.Key) select p).SingleOrDefault();
            if (existingModPack != null) throw new ModPackAlreadyExistsException();

            _list.Add(modPack);
            OnPropertyChanged(nameof(List));
        }

        public void UpdateModPack(ModPack modPack)
        {
            ModPack? existingModPack = (from p in _list where p.Key.Equals(modPack.Key) select p).SingleOrDefault();
            if (existingModPack == null)
            {
                AddModPack(modPack);
                return;
            }

            _list.Remove(existingModPack);
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
