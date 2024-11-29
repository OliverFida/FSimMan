using OF.Base.Objects;
using OF.FSimMan.Game.Exceptions;

namespace OF.FSimMan.Game
{
    public class ModPacksEditor : EditorBase<ModPacks>
    {
        #region Constructor
        public ModPacksEditor(ModPacks objectToEdit) : base(objectToEdit) { }
        #endregion

        #region Methods PUBLIC
        public void AddModPack(ModPack modPack) => AddModPack(modPack, false);
        public void AddModPack(ModPack modPack, bool ignoreAlreadyExisting)
        {
            ObjectToEdit.BeginEdit();

            ModPack? existingModPack = (from p in ObjectToEdit.List where p.Guid.Equals(modPack.Guid) select p).SingleOrDefault();
            if (existingModPack is not null) throw new ModPackAlreadyExistsException();

            ObjectToEdit._list.Add(modPack);

            ObjectToEdit.EndEdit();
            OnPropertyChanged(nameof(ObjectToEdit.List));
        }

        public void UpdateModPack(ModPack modPack)
        {
            ObjectToEdit.BeginEdit();

            ModPack? existingModPack = (from p in ObjectToEdit.List where p.Guid.Equals(modPack.Guid) select p).SingleOrDefault();
            if (existingModPack is null) throw new ModPackNotExistingException();

            ObjectToEdit._list.Remove(existingModPack);
            ObjectToEdit._list.Add(modPack);

            ObjectToEdit.EndEdit();
            OnPropertyChanged(nameof(ObjectToEdit.List));
        }

        public void RemoveModPack(ModPack modPack)
        {
            ObjectToEdit.BeginEdit();

            ObjectToEdit._list.Remove(modPack);

            ObjectToEdit.EndEdit();
            OnPropertyChanged(nameof(ObjectToEdit.List));
        }
        #endregion
    }
}
