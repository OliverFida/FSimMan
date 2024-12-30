using Microsoft.EntityFrameworkCore;
using OF.FSimMan.Management;

namespace OF.FSimMan.Database.Context
{
    public class SettingsDbContext : DatabaseContextBase
    {
        #region Properties
        public DbSet<AppSettingsData> AppSettings => Set<AppSettingsData>();
        #endregion

        #region Methods PUBLIC
        public AppSettings ReadAppSettings()
        {
            AppSettingsData? temp = ReadAppSettingsData();
            if (temp is null) return new AppSettings();

            return temp.FromData();
        }

        public AppSettings StoreAppSettings(AppSettings value)
        {
            AppSettingsData? existing = ReadAppSettingsData();

            if (existing is null)
            {
                var temp = new AppSettingsData();
                temp.ToData(value);
                AppSettings.Add(temp);
            }
            else
            {
                existing.ToData(value);
            }

            SaveChanges();

            if (existing is null) return ReadAppSettings();
            return value;
        }
        #endregion

        #region Methods PRIVATE
        private AppSettingsData? ReadAppSettingsData()
        {
            return AppSettings.FirstOrDefault();
        }
        #endregion
    }
}
