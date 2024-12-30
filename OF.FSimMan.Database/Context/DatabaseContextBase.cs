using Microsoft.EntityFrameworkCore;

namespace OF.FSimMan.Database.Context
{
    public abstract class DatabaseContextBase : DbContext
    {
        #region Methods PROTECTED
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbFileName = "dev.db";
            string dbFilePath = Path.Combine(CurrentApplication.CONFIG_PATH, dbFileName);
            optionsBuilder.UseSqlite($"Data Source={dbFilePath}");
        }
        #endregion
    }
}
