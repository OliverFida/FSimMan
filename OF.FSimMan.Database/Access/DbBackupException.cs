using OF.Base.Objects;

namespace OF.FSimMan.Database.Access
{
    public class DbBackupException : OfException
    {
        #region Constructor
        public DbBackupException(string reason) : base($"Database-Backup could not be created:" + Environment.NewLine + reason) { }
        #endregion
    }
}
