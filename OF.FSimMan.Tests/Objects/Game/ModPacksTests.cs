using OF.FSimMan.Game;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Objects.Game
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class ModPacksTests
    {
        private static readonly Management.Game _game = Management.Game.FarmingSim22;

        [TestMethod]
        public void ModPacks_Self()
        {
            ModPacks packs = new ModPacks();
            ModPacksEditor mpe = new ModPacksEditor(packs);
            Assert.AreEqual(0, mpe.ObjectToEdit.List.Count);

            ModPack pack = new ModPack(_game);

            mpe.AddModPack(pack);
            Assert.AreEqual(1, mpe.ObjectToEdit.List.Count);

            // OFDO: ModPacks_Self
            //string orgTitle = pack.Title;
            //string newTitle = "Test title";
            //mpe.TriggerBeginEdit();
            //mpe.ObjectToEdit.List.First().Title = newTitle;
            //Assert.AreEqual(newTitle, mpe.ObjectToEdit.List.First().Title);


            //mpe.TriggerCancelEdit();
            //Assert.AreEqual(orgTitle, mpe.ObjectToEdit.List.First().Title);

            //mpe.TriggerBeginEdit();
            //mpe.ObjectToEdit.List.First().Title = newTitle;
            //mpe.TriggerEndEdit();
            //Assert.AreEqual(newTitle, mpe.ObjectToEdit.List.First().Title);
        }
    }
}
