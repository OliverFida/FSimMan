using OF.FSimMan.Client.Game.Fs;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Clients.Game.Fs
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class Fs22ClientTests : GameClientTestsBase
    {
        [TestMethod]
        public void Fs22Client_Self()
        {
            Fs22Client c = new Fs22Client();
            GameClient_New(c);
            // OFDO: Fs22Client_Self
        }
    }
}
