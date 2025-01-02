using Microsoft.EntityFrameworkCore;
using OF.FSimMan.Management;
using OF.FSimMan.Management.Games;
using OF.FSimMan.Management.Games.Fs;

namespace OF.FSimMan.Database.Data
{
    public class SettingsDbContext : Base.EfCore.SqLite.DbContextBase
    {
        #region Properties
        public DbSet<AppSettingsData> AppSettings => Set<AppSettingsData>();
        public DbSet<GameSettingsFs22Data> GameSettingsFs22 => Set<GameSettingsFs22Data>();
        public DbSet<GameSettingsFs25Data> GameSettingsFs25 => Set<GameSettingsFs25Data>();
        public DbSet<GameSettingsStartArgumentsData> GameSettingsStartArguments => Set<GameSettingsStartArgumentsData>();
        #endregion

        #region Constructor
        public SettingsDbContext() : base(Path.Combine(CurrentApplication.CONFIG_DATABASE_PATH)) { }
        #endregion
    }
}
