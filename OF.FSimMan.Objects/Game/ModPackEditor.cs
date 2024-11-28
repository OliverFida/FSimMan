using OF.Base.Objects;

namespace OF.FSimMan.Game
{
    public class ModPackEditor : EditorBase<ModPack>
    {
        #region Constructor
        public ModPackEditor(ModPack objectToEdit) : base(objectToEdit) { }
        #endregion

        #region Methods PUBLIC
        public void AddMod(Mod mod)
        {
            // OFDO: Check over
            ObjectToEdit.BeginEdit();

            // OFDO: Check mod already added

            ObjectToEdit._mods.Add(mod);

            ObjectToEdit.EndEdit();
            OnPropertyChanged(nameof(ObjectToEdit.Mods));
        }

        public void RemoveMod(Mod mod)
        {
            // OFDO: Check over
            ObjectToEdit.BeginEdit();

            ObjectToEdit._mods.Remove(mod);

            ObjectToEdit.EndEdit();
            OnPropertyChanged(nameof(ObjectToEdit.Mods));
        }
        #endregion
    }
}
