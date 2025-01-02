using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using OF.Base.Objects;
using OF.FSimMan.Database.Data;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games;

namespace OF.FSimMan.Database.Access
{
    public class SettingsDbAccess : OF.Base.EfCore.SqLite.DbAccessBase<SettingsDbContext>, ISingleton<SettingsDbAccess>
    {
        #region Constructor
        private SettingsDbAccess(bool doAutoMigrate) : base(doAutoMigrate) { }
        #endregion

        #region Methods PUBLIC
        public AppSettings ReadAppSettings()
        {
            using SettingsDbContext db = CreateContext();

            AppSettingsData? temp = ReadAppSettingsData(db);

            return temp?.FromData() ?? new AppSettings();
        }

        public AppSettings StoreAppSettings(AppSettings newAppSettings)
        {
            using SettingsDbContext db = CreateContext();
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                AppSettingsData? existingAppSettingsData = ReadAppSettingsData(db);

                AppSettingsData? newAppSettingsData = new AppSettingsData();
                newAppSettingsData.ToData(newAppSettings);
                EntityEntry<AppSettingsData> entityEntry;
                if (existingAppSettingsData is null) entityEntry = db.AppSettings.Add(newAppSettingsData);
                else
                {
                    db.AppSettings.Entry(existingAppSettingsData).CurrentValues.SetValues(newAppSettingsData);
                    AppendGameSettingsData(db, existingAppSettingsData, newAppSettingsData);
                    entityEntry = db.AppSettings.Entry(existingAppSettingsData);
                }

                db.SaveChanges();
                transaction.Commit();
                return entityEntry.Entity.FromData();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        #endregion

        #region Methods PRIVATE
        #region AppSettings
        private AppSettingsData? ReadAppSettingsData(SettingsDbContext db)
        {
            return db.AppSettings.Select(s => s).Include(s => s.GameSettings).ThenInclude(s => ((GameSettingsDataBase)s).StartArguments).SingleOrDefault();
        }
        #endregion
        #region GameSettings
        private void AppendGameSettingsData(SettingsDbContext db, AppSettingsData existingAppSettingsData, AppSettingsData newAppSettingsData)
        {
            existingAppSettingsData.GameSettings.Clear();
            existingAppSettingsData.GameSettings.AddRange(newAppSettingsData.GameSettings);
        }
        #endregion
        #endregion

        #region ISingleton
        private static SettingsDbAccess? _instance;
        public static SettingsDbAccess Instance
        {
            get
            {
                if (_instance is null) _instance = new SettingsDbAccess(true);
                return _instance;
            }
        }
        #endregion
    }
}
