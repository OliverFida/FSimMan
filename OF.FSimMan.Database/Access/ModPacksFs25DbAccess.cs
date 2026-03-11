using OF.FSimMan.Base;
using OF.FSimMan.Database.Data;

namespace OF.FSimMan.Database.Access
{
    public class ModPacksFs25DbAccess : ModPacksDbAccessBase<ModPacksFs25DbContext>, ICustomSingleton<ModPacksFs25DbAccess>
    {
        #region Constructor
        private ModPacksFs25DbAccess(bool doAutoMigrate) : base(
            doAutoMigrate,
            backupBeforeMigrations: new string[]
            {
                "20260226191359_MigrationToGuid",
            }
        )
        { }
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
