namespace OF.FSimMan.Management.Exceptions
{
    public class GamePathAutodetectionFailedWarning : WarningAsException
    {
        public GamePathAutodetectionFailedWarning() : base("Game Path autodection failed!" + Environment.NewLine + "Please select manually.") { }
    }
}
