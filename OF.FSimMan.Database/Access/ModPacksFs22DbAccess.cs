using OF.Base.Objects;
using OF.FSimMan.Database.Data;

namespace OF.FSimMan.Database.Access
{
    public class ModPacksFs22DbAccess : ModPacksDbAccessBase<ModPacksFs22DbContext>, ISingleton<ModPacksFs22DbAccess>
    {
        #region Constructor
        private ModPacksFs22DbAccess(bool doAutoMigrate) : base(doAutoMigrate) { }
        #endregion

        #region ISingleton
        private static ModPacksFs22DbAccess? _instance;
        public static ModPacksFs22DbAccess Instance
        {
            get
            {
                if (_instance is null) _instance = new ModPacksFs22DbAccess(true);
                return _instance;
            }
        }
        #endregion
    }
}
