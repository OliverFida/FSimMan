namespace OF.FSimMan.Client.Game.Fs
{
    public abstract class FsClientBase : GameClientBase
    {
        #region Constructor
        public FsClientBase(FSimMan.Management.Game game) : base(game) { }
        public FsClientBase(FSimMan.Management.Game game, bool doInitialize) : base(game, doInitialize) { }
        #endregion
    }
}
