namespace OF.FSimMan.Database.Data
{
    public class ModPacksFs25DbContext : ModPacksDbContextBase
    {
        #region Constructor
        public ModPacksFs25DbContext() : base(Management.Game.FarmingSim25) { }
        #endregion
    }
}
