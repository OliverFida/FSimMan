using Microsoft.EntityFrameworkCore;

namespace OF.FSimMan.Database.Access
{
    public abstract class DbAccessBase<TContext> : OF.Base.EfCore.SqLite.DbAccessBase<TContext> where TContext : OF.Base.EfCore.SqLite.DbContextBase, new()
    {
        #region Properties
        protected readonly HashSet<string> _backupBeforeMigrations;
        private bool _backupCleanupRan = false;
        private int _oldBackupsLimitDays;
        #endregion

        #region Constructor
        protected DbAccessBase(bool doAutoMigrate, IEnumerable<string>? backupBeforeMigrations = null, int oldBackupsLimitDays = 30) : base(false)
        {
            _backupBeforeMigrations = backupBeforeMigrations is not null ? new HashSet<string>(backupBeforeMigrations) : new HashSet<string>();
            _oldBackupsLimitDays = oldBackupsLimitDays;

            if (doAutoMigrate)
            {
                DeleteOldBackups();
                Migrate();
            }
        }
        #endregion

        #region Methods PUBLIC
        public override void Migrate()
        {
            using TContext db = CreateContext();

            List<string> pending = db.Database.GetPendingMigrations().ToList();

            foreach (string migration in pending)
            {
                if (_backupBeforeMigrations.Any(t => migration.Equals(t))) BackupDatabase(db, migration);

                db.Database.Migrate(migration);
            }
        }
        #endregion

        #region Methods PRIVATE
        private void BackupDatabase(TContext db, string migration)
        {
            string sourcePath = db.DataSource;
            if (!File.Exists(sourcePath)) throw new DbBackupException("DB-File does not exist");

            string backupFileName = Path.GetFileNameWithoutExtension(sourcePath) +
                "-before-" +
                migration +
                ".bak";

            string backupPath = Path.Combine(
                CurrentApplication.CONFIG_BACKUP_PATH,
                backupFileName
            );

            File.Copy(sourcePath, backupPath);
        }

        private void DeleteOldBackups()
        {
            if (_backupCleanupRan) return;

            using TContext db = CreateContext();

            DirectoryInfo directoryInfo = new DirectoryInfo(CurrentApplication.CONFIG_BACKUP_PATH);

            string dbFileName = Path.GetFileNameWithoutExtension(db.DataSource);
            List<FileInfo> fileInfos = directoryInfo.GetFiles($"{dbFileName}-before-*.bak").ToList();

            DateTime limit = DateTime.Now.AddDays(_oldBackupsLimitDays * -1);

            foreach (FileInfo fileInfo in fileInfos)
            {
                if (fileInfo.CreationTime.CompareTo(limit) < 0)
                {
                    fileInfo.Delete();
                }
            }

            _backupCleanupRan = true;
        }
        #endregion
    }
}
