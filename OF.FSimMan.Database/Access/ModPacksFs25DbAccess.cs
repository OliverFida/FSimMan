using OF.Base.Objects;
using OF.FSimMan.Database.Data;

namespace OF.FSimMan.Database.Access
{
    public class ModPacksFs25DbAccess : ModPacksDbAccessBase<ModPacksFs25DbContext>, ISingleton<ModPacksFs25DbAccess>
    {
        #region Constructor
        private ModPacksFs25DbAccess(bool doAutoMigrate) : base(doAutoMigrate) { }
        #endregion

        #region ISingleton
        private static ModPacksFs25DbAccess? _instance;
        public static ModPacksFs25DbAccess Instance
        {
            get
            {
                if (_instance is null) _instance = new ModPacksFs25DbAccess(true);
                return _instance;
            }
        }
        #endregion
    }
}
