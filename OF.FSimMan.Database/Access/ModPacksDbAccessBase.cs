using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using OF.FSimMan.Database.Data;
using OF.FSimMan.Game;

namespace OF.FSimMan.Database.Access
{
    public interface IModPacksDbAccess
    {
        public ModPacks ReadModPacks();
        public ModPack StoreModPack(ModPack newModPack);
        public ModPacks BulkStoreModPacks(ModPacks newModPacks);
    }

    public abstract class ModPacksDbAccessBase<TContext> : OF.Base.EfCore.SqLite.DbAccessBase<TContext>, IModPacksDbAccess where TContext : ModPacksDbContextBase, new()
    {
        #region Constructor
        protected ModPacksDbAccessBase(bool doAutoMigrate) : base(doAutoMigrate) { }
        #endregion

        #region Methods PUBLIC
        public ModPacks ReadModPacks()
        {
            using ModPacksDbContextBase db = CreateContext();

            ModPacks modPacks = new ModPacks();
            ModPacksEditor modPacksEditor = new ModPacksEditor(modPacks);

            ReadModPacksData(db).ForEach(p => modPacksEditor.AddModPack(p.FromData()));

            return modPacks;
        }

        public ModPack StoreModPack(ModPack newModPack)
        {
            using ModPacksDbContextBase db = CreateContext();
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                ModPackData? existingModPackData = db.ModPacks.Where(d => d.Id.Equals(newModPack.Id)).Include(d => d.Mods).Include(d => d.Dlcs).SingleOrDefault();

                ModPackData newModPackData = new ModPackData();
                newModPackData.ToData(newModPack);
                EntityEntry<ModPackData> entityEntry;
                if (existingModPackData is null) entityEntry = db.ModPacks.Add(newModPackData);
                else
                {
                    db.Entry(existingModPackData).CurrentValues.SetValues(newModPackData);
                    AppendModsData(db, existingModPackData, newModPackData);
                    AddendDlcsData(db, existingModPackData, newModPackData);
                    entityEntry = db.Entry(existingModPackData);
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

        public ModPacks BulkStoreModPacks(ModPacks newModPacks)
        {
            using ModPacksDbContextBase db = CreateContext();
            using IDbContextTransaction transaction = db.Database.BeginTransaction();

            try
            {
                List<int> existingIds = db.ModPacks
                    .Where(d => newModPacks.List.Select(p => p.Id).Contains(d.Id))
                    .Select(d => d.Id)
                    .ToList();

                List<ModPackData> toDelete = db.ModPacks.Where(d => !existingIds.Contains(d.Id)).ToList();
                List<ModPackData> toUpdate = newModPacks.List.Where(p => existingIds.Contains(p.Id)).Select(p => { var t = new ModPackData(); t.ToData(p); return t; }).ToList();
                List<ModPackData> toInsert = newModPacks.List.Where(p => p.Id.Equals(0)).Select(p => { var t = new ModPackData(); t.ToData(p); return t; }).ToList();

                // OFDOL: Not real bulk
                //db.BulkDelete(toDelete);
                //db.BulkUpdate(toUpdate);
                //db.BulkInsert(toInsert);

                toDelete.ForEach(p => db.ModPacks.Remove(p));
                toUpdate.ForEach(p =>
                {
                    ModPackData existingModPackData = db.ModPacks.Local.SingleOrDefault(d => d.Id.Equals(p.Id)) ?? db.ModPacks.Find(p.Id)!;
                    db.ModPacks.Entry(existingModPackData).CurrentValues.SetValues(p);
                    AppendModsData(db, existingModPackData, p);
                    AddendDlcsData(db, existingModPackData, p);
                });
                toInsert.ForEach(p => db.ModPacks.Add(p));

                db.SaveChanges();
                transaction.Commit();
                return ReadModPacks();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
        #endregion

        #region Methods PRIVATE
        #region ModPack
        private List<ModPackData> ReadModPacksData(ModPacksDbContextBase db)
        {
            return db.ModPacks.Include(d => d.Mods).Include(d => d.Dlcs).ToList();
        }
        #endregion

        #region Mod
        private void AppendModsData(ModPacksDbContextBase db, ModPackData existingModPackData, ModPackData newModPackData)
        {
            existingModPackData.Mods.Clear();
            existingModPackData.Mods.AddRange(newModPackData.Mods);
        }

        private void AddendDlcsData(ModPacksDbContextBase db, ModPackData existingModPackData, ModPackData newModPackData)
        {
            existingModPackData.Dlcs.Clear();
            existingModPackData.Dlcs.AddRange(newModPackData.Dlcs);
        }
        #endregion
        #endregion
    }
}
