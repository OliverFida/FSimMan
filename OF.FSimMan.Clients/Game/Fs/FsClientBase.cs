namespace OF.FSimMan.Client.Game.Fs
{
    public abstract class FsClientBase : GameClientBase
    {
        #region Constructor
        public FsClientBase(FSimMan.Management.Game game) : base(game) { }
        #endregion

        #region Methods PROTECTED
        protected override void SetGameModFolder()
        {
            // OFDO
        }
        #endregion
    }
}
