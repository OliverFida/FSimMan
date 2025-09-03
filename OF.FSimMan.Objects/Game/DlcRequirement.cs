using OF.Base.Objects;

namespace OF.FSimMan.Game
{
    public class DlcRequirement : EditableObject
    {
        internal ModPack _parent;
        #region Properties
        internal string _fileName;
        public string FileName
        {
            get => _fileName;
        }
        #endregion

        #region Constructor
        public DlcRequirement(ModPack parent, string fileName)
        {
            _parent = parent;
            _fileName = fileName;
        }
        #endregion
    }
}
