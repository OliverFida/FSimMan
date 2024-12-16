using OF.FSimMan.Api.GitHub;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Objects.Api.GitHub
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class GitHubReleaseAssetDataTests
    {
        [TestMethod]
        public void GitHubReleaseAssetData_Self()
        {
            GitHubReleaseAssetData data = new GitHubReleaseAssetData();

            Assert.AreEqual(string.Empty, data.Name);
            Assert.AreEqual(string.Empty, data.DownloadUrl);
        }
    }
}
