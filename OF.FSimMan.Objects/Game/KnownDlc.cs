namespace OF.FSimMan.Game
{
    public sealed class KnownDlc
    {
        #region Properties
        public static List<KnownDlc> List = new List<KnownDlc>()
        {
            new KnownDlc(Management.Game.FarmingSim25, "MacDon Pack", "macDonPack"),
            new KnownDlc(Management.Game.FarmingSim25, "Straw Harvest Pack", "strawHarvestPack"),
            new KnownDlc(Management.Game.FarmingSim25, "Precision Farming 3.0", "precisionFarmingPack"),
            new KnownDlc(Management.Game.FarmingSim25, "Mercedes-Benz Trucks Pack", "daimlerTruckPack"),
            new KnownDlc(Management.Game.FarmingSim25, "Plains & Prairies Pack", "plainsAndPrairiesPack"),
            new KnownDlc(Management.Game.FarmingSim25, "NEXAT Pack", "nexatPack"),
            new KnownDlc(Management.Game.FarmingSim25, "New Holland CR11 Gold Edition", "extraContentNewHollandCR11"),
            //new KnownDlc(Management.Game.FarmingSim25, "Highlands Fishing Expansion", "daimlerTruckPack"),
        };


        private Management.Game _game;

        private string _title;
        public string Title { get => _title; }

        private string _fileName;
        public string FileName { get => _fileName; }
        #endregion

        #region Constructor
        private KnownDlc(Management.Game game, string title, string fileName)
        {
            _game = game;
            _title = title;
            _fileName = fileName;
        }
        #endregion

        #region Methods PUBLIC
        public static KnownDlc? GetByFileName(string fileName)
        {
            return List.Where(d => d.FileName.Equals(fileName)).SingleOrDefault();
        }

        public static List<KnownDlc> GetByGame(Management.Game game)
        {
            return List.Where(d => d._game.Equals(game)).ToList();
        }
        #endregion
    }
}
