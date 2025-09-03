using OF.Base.Objects;

namespace OF.FSimMan.Game
{
    public class DlcRequirement : EditableObject
    {
        // TODOI: See if this is getting exported to .fsmmmp

        internal ModPack _parent;

        #region Properties
        internal KnownDlc? _dlc;
        public KnownDlc? Dlc
        {
            get => _dlc;
        }
        #endregion

        #region Constructor
        public DlcRequirement(ModPack parent, KnownDlc dlc)
        {
            _parent = parent;
            _dlc = dlc;
        }

        internal DlcRequirement(ModPack parent, string fileName)
        {
            _parent = parent;
            _dlc = KnownDlc.GetByFileName(fileName);
        }
        #endregion
    }
}
