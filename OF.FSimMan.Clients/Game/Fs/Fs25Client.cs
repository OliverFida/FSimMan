namespace OF.FSimMan.Client.Game.Fs
{
    public class Fs25Client : FsClientBase
    {
        #region Constructor
        public Fs25Client() : base(FSimMan.Management.Game.FarmingSim25) { }
        #endregion

        #region Methods PROTECTED
        protected override void ReadGameSettings()
        {
            // OFDO
        }

        protected override void SetGameModFolder()
        {
            // OFDO
        }

        protected override void StoreGameSettings()
        {
            // OFDO
        }
        #endregion
    }
}
