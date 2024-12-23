using OF.Base.Objects;

namespace OF.FSimMan.Logging
{
    public class LoggingExceptionWithAnalyticsData : OfException
    {
        LoggingAnalyticsData AnalyticsData { get; }

        public LoggingExceptionWithAnalyticsData(string exceptionMessage) : this(exceptionMessage, null) { }
        public LoggingExceptionWithAnalyticsData(Exception innerException) : this("Analytic Exception", innerException) { }
        public LoggingExceptionWithAnalyticsData(string exceptionMessage, Exception? innerException) : base(exceptionMessage, innerException)
        {
            AnalyticsData = new LoggingAnalyticsData();
        }
    }
}
