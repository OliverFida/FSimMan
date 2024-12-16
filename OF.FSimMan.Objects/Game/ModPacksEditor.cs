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
        public void TriggerBeginEdit() => BeginEdit();
        public void TriggerCancelEdit() => CancelEdit();
        public void TriggerEndEdit() => EndEdit();

        public void AddModPack(ModPack modPack) => AddModPack(modPack, false);
        public void AddModPack(ModPack modPack, bool ignoreAlreadyExisting)
        {
            try
            {
                BeginEdit();

                ModPack? existingModPack = (from p in ObjectToEdit.List where p.Guid.Equals(modPack.Guid) select p).SingleOrDefault();
                if (!ignoreAlreadyExisting && existingModPack is not null) throw new ModPackAlreadyExistsException();

                if (ignoreAlreadyExisting && existingModPack is not null) ObjectToEdit._list.Remove(existingModPack);
                ObjectToEdit._list.Add(modPack);

                EndEdit();
                OnPropertyChanged(nameof(ObjectToEdit.List));
            }
            catch
            {
                CancelEdit();
                throw;
            }
        }

        public void RemoveModPack(ModPack modPack)
        {
            try
            {
                BeginEdit();

                ObjectToEdit._list.Remove(modPack);

                EndEdit();
                OnPropertyChanged(nameof(ObjectToEdit.List));
            }
            catch
            {
                CancelEdit();
                throw;
            }
        }
        #endregion
    }
}
