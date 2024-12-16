using OF.FSimMan.Api.GitHub;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Objects.Api.GitHub
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class GitHubReleaseDataTests
    {
        [TestMethod]
        public void GitHubReleaseData_Self()
        {
            GitHubReleaseData data = new GitHubReleaseData();

            Assert.AreEqual(string.Empty, data.TagName);
            Assert.IsTrue(data.IsDraft);
            Assert.IsTrue(data.IsPreRelease);
            Assert.AreEqual([], data.Assets);
        }
    }
}
