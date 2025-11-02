namespace OF.FSimMan.Management.Exceptions
{
    public class DataPathAutodetectionFailedWarning : WarningAsException
    {
        public DataPathAutodetectionFailedWarning() : base("Data Path autodection failed!" + Environment.NewLine + "Please select manually.") { }
    }
}
