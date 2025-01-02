namespace OF.FSimMan.Database.Data
{
    public class ModPacksFs22DbContext : ModPacksDbContextBase
    {
        #region Constructor
        public ModPacksFs22DbContext() : base(Management.Game.FarmingSim22) { }
        #endregion
    }
}
