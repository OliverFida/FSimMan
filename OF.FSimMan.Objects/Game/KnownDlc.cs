namespace OF.FSimMan.Game
{
    public abstract class KnownDlc
    {
        #region Properties
        private string _title;
        public string Title { get => _title; }

        private string _fileName;
        public string FileName { get => _fileName; }
        #endregion

        #region Constructor
        protected KnownDlc(string title, string fileName)
        {
            _title = title;
            _fileName = fileName;
        }
        #endregion
    }
}
