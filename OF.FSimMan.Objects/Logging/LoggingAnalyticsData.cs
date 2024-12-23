using System.Net;

namespace OF.FSimMan.Logging
{
    public class LoggingAnalyticsData
    {
        string SystemUserName = System.Environment.UserName;
        string SystemName = System.Environment.MachineName;
        Version? ApplicationVersion = CurrentApplication.AssemblyVersion;
        IPAddress? SystemPublicIpV4Address;
        string OsVersion = System.Runtime.InteropServices.RuntimeInformation.OSDescription;
        string OsArchitecture = System.Runtime.InteropServices.RuntimeInformation.OSArchitecture.ToString();

        public LoggingAnalyticsData()
        {
            var task = GetExternalIpAddress();
            task.Wait();
            SystemPublicIpV4Address = task.Result;
        }

        private static async Task<IPAddress?> GetExternalIpAddress()
        {
            var externalIpString = (await new HttpClient().GetStringAsync("http://icanhazip.com"))
                .Replace("\\r\\n", "").Replace("\\n", "").Trim();
            if (!IPAddress.TryParse(externalIpString, out var ipAddress)) return null;
            return ipAddress;
        }
    }
}
