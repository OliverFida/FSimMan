namespace OF.FSimMan.Game.Fs.Fs25
{
    public sealed class Fs25KnownDlc : KnownDlc
    {
        #region Static List
        public static readonly Fs25KnownDlc MacDonPack = new Fs25KnownDlc("MacDon Pack", "macDonPack.dlc");
        public static readonly Fs25KnownDlc StrawHarvestPack = new Fs25KnownDlc("Straw Harves Pack", "strawHarvestPack.dlc");
        #endregion

        #region Constructor
        private Fs25KnownDlc(string title, string fileName) : base(title, fileName) { }
        #endregion
    }
}
