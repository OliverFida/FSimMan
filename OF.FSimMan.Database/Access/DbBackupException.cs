using CLS.Core;

namespace OF.FSimMan.Database.Access
{
    public class DbBackupException: ClsException
    {
        #region Constructor
        public DbBackupException(string reason) : base($"Database-Backup could not be created:" + Environment.NewLine + reason) { }
        #endregion
    }
}
