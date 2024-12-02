using OF.FSimMan.Game;

namespace OF.FSimMan.Client.Game.Fs
{
    public abstract class FsEditModPackClientBase : EditModPackClientBase
    {
        #region Constructor
        public FsEditModPackClientBase(ModPack modPack, FsClientBase gameClient) : base(modPack, gameClient) { }
        #endregion
    }
}
