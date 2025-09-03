using Microsoft.EntityFrameworkCore;
using OF.FSimMan.Game;

namespace OF.FSimMan.Database.Data
{
    public abstract class ModPacksDbContextBase : Base.EfCore.SqLite.DbContextBase
    {
        #region Properties
        public DbSet<ModPackData> ModPacks => Set<ModPackData>();
        public DbSet<ModData> Mods => Set<ModData>();
        public DbSet<DlcRequirementData> Dlcs => Set<DlcRequirementData>();
        #endregion

        #region Constructor
        public ModPacksDbContextBase(Management.Game game) : base(Path.Combine(CurrentApplication.GetModPackDatabasePath(game))) { }
        #endregion
    }
}
