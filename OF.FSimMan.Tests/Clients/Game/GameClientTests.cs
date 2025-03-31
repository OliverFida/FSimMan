using OF.FSimMan.Client.Game;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Clients.Game
{
    [ExcludeFromCodeCoverage]
    public abstract class GameClientTestsBase : FSimManTestsBase
    {
        protected void GameClient_New(IGameClient c)
        {
            bool isGameStateChanged = false;
            EventHandler gameStateChanged = (object? sender, EventArgs e) =>
            {
                isGameStateChanged = true;
            };

            Assert.AreEqual(0, c.ModPacks.List.Count);
            Assert.AreEqual(null, c.SelectedModPack);
            Assert.IsFalse(isGameStateChanged);
            // OFDO: GameClient_New
        }
    }
}
