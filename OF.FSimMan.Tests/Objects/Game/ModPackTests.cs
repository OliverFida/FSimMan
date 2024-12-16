using OF.FSimMan.Game;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Objects.Game
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class ModPackTests : FSimManTestsBase
    {
        private static readonly FSimMan.Management.Game _game = FSimMan.Management.Game.FarmingSim22;

        [TestMethod]
        public void ModPack_New()
        {
            ModPackData mpd = new ModPackData();
            ModPack pack = mpd.FromData();
            Guid guid = pack.Guid;
            string modPackDirectoryPath = Path.Combine(CurrentApplication.MODPACKS_PATH, FSimMan.Management.Game.None.ToString(), guid.ToString());
            string modsDirectoryPath = Path.Combine(modPackDirectoryPath, "mods");
            string modsTempDirectoryPath = Path.Combine(modPackDirectoryPath, "modsTemp");
            string modIconsDirectoryPath = Path.Combine(modPackDirectoryPath, "modIcons");

            Assert.AreEqual(string.Empty, pack.Title);
            Assert.AreEqual(null, pack.Version);
            Assert.AreEqual(false, pack.IsVersionVisible);
            Assert.AreEqual(null, pack.Author);
            Assert.AreEqual(null, pack.Description);
            Assert.AreEqual(null, pack.ImageSource);

            Assert.AreEqual(0, pack.Mods.Count);

            Assert.AreEqual(modPackDirectoryPath, pack.ModPackDirectoryPath);
            Assert.AreEqual(modsDirectoryPath, pack.ModsDirectoryPath);
            Assert.AreEqual(modsTempDirectoryPath, pack.ModsTempDirectoryPath);
            Assert.AreEqual(modIconsDirectoryPath, pack.ModIconsDirectoryPath);

            pack = new ModPack(_game);

            guid = pack.Guid;
            string title = "New modpack";
            string? version = null;
            bool isVersionVisible = false;
            string? author = null;
            string? description = null;
            string? imageSource = null;
            modPackDirectoryPath = Path.Combine(CurrentApplication.MODPACKS_PATH, _game.ToString(), guid.ToString());
            modsDirectoryPath = Path.Combine(modPackDirectoryPath, "mods");
            modsTempDirectoryPath = Path.Combine(modPackDirectoryPath, "modsTemp");
            modIconsDirectoryPath = Path.Combine(modPackDirectoryPath, "modIcons");

            Assert.AreEqual(title, pack.Title);
            Assert.AreEqual(version, pack.Version);
            Assert.AreEqual(isVersionVisible, pack.IsVersionVisible);
            Assert.AreEqual(author, pack.Author);
            Assert.AreEqual(description, pack.Description);
            Assert.AreEqual(imageSource, pack.ImageSource);

            Assert.AreEqual(0, pack.Mods.Count);

            Assert.AreEqual(modPackDirectoryPath, pack.ModPackDirectoryPath);
            Assert.AreEqual(modsDirectoryPath, pack.ModsDirectoryPath);
            Assert.AreEqual(modsTempDirectoryPath, pack.ModsTempDirectoryPath);
            Assert.AreEqual(modIconsDirectoryPath, pack.ModIconsDirectoryPath);
        }

        [TestMethod]
        public void ModPack_Custom()
        {
            ModPack pack = new ModPack(_game);

            Guid guid = pack.Guid;
            string title = "Test modpack";
            string? version = "0.1.0";
            bool isVersionVisible = true;
            string? author = "Oliver Fida";
            string? description = "Wonderful description";
            string? imageSource = "hudriWudri.png";
            Mod tempMod = new Mod(pack, "testFileName.zip");
            string modPackDirectoryPath = Path.Combine(CurrentApplication.MODPACKS_PATH, _game.ToString(), guid.ToString());
            string modsDirectoryPath = Path.Combine(modPackDirectoryPath, "mods");
            string modsTempDirectoryPath = Path.Combine(modPackDirectoryPath, "modsTemp");
            string modIconsDirectoryPath = Path.Combine(modPackDirectoryPath, "modIcons");

            pack.Title = title;
            pack.Version = version;
            pack.Author = author;
            pack.Description = description;
            pack.ImageSource = imageSource;
            pack.Mods.Add(tempMod);

            Assert.AreEqual(title, pack.Title);
            Assert.AreEqual(version, pack.Version);
            Assert.AreEqual(isVersionVisible, pack.IsVersionVisible);
            Assert.AreEqual(author, pack.Author);
            Assert.AreEqual(description, pack.Description);
            Assert.AreEqual(imageSource, pack.ImageSource);

            Assert.AreEqual(1, pack.Mods.Count);

            Assert.AreEqual(modPackDirectoryPath, pack.ModPackDirectoryPath);
            Assert.AreEqual(modsDirectoryPath, pack.ModsDirectoryPath);
            Assert.AreEqual(modsTempDirectoryPath, pack.ModsTempDirectoryPath);
            Assert.AreEqual(modIconsDirectoryPath, pack.ModIconsDirectoryPath);
        }

        [TestMethod]
        public void ModPack_Editor()
        {
            ModPack pack = new ModPack(_game);

            Guid orgGuid = pack.Guid;
            string orgTitle = "New modpack";
            string? orgVersion = null;
            string? orgAuthor = null;
            string? orgDescription = null;
            string? orgImageSource = null;
            string orgModPackDirectoryPath = Path.Combine(CurrentApplication.MODPACKS_PATH, _game.ToString(), orgGuid.ToString());
            string orgModsDirectoryPath = Path.Combine(orgModPackDirectoryPath, "mods");
            string orgModsTempDirectoryPath = Path.Combine(orgModPackDirectoryPath, "modsTemp");
            string orgModIconsDirectoryPath = Path.Combine(orgModPackDirectoryPath, "modIcons");

            string newTitle = "Test modpack";
            string? newVersion = "0.1.1";
            string? newAuthor = "Oliver Fida 2";
            string? newDescription = "Wonderful description. Now extended.";
            string? newImageSource = "hudriWudri2-0.png";
            Mod tempMod = new Mod(pack, "testFileName.zip");

            ModPackEditor mpe = new ModPackEditor(pack);
            mpe.TriggerBeginEdit();
            mpe.ObjectToEdit.Title = newTitle;
            mpe.ObjectToEdit.Version = newVersion;
            mpe.ObjectToEdit.Author = newAuthor;
            mpe.ObjectToEdit.Description = newDescription;
            mpe.ObjectToEdit.ImageSource = newImageSource;
            Assert.AreEqual(orgGuid.ToString(), pack.Guid.ToString());
            Assert.AreEqual(newTitle, pack.Title);
            Assert.AreEqual(newVersion, pack.Version);
            Assert.AreEqual(true, pack.IsVersionVisible);
            Assert.AreEqual(newAuthor, pack.Author);
            Assert.AreEqual(newDescription, pack.Description);
            Assert.AreEqual(newImageSource, pack.ImageSource);

            Assert.AreEqual(orgModPackDirectoryPath, pack.ModPackDirectoryPath);
            Assert.AreEqual(orgModsDirectoryPath, pack.ModsDirectoryPath);
            Assert.AreEqual(orgModsTempDirectoryPath, pack.ModsTempDirectoryPath);
            Assert.AreEqual(orgModIconsDirectoryPath, pack.ModIconsDirectoryPath);

            mpe.TriggerCancelEdit();
            Assert.AreEqual(orgGuid.ToString(), pack.Guid.ToString());
            Assert.AreEqual(orgTitle, pack.Title);
            Assert.AreEqual(orgVersion, pack.Version);
            Assert.AreEqual(false, pack.IsVersionVisible);
            Assert.AreEqual(orgAuthor, pack.Author);
            Assert.AreEqual(orgDescription, pack.Description);
            Assert.AreEqual(orgImageSource, pack.ImageSource);

            Assert.AreEqual(orgModPackDirectoryPath, pack.ModPackDirectoryPath);
            Assert.AreEqual(orgModsDirectoryPath, pack.ModsDirectoryPath);
            Assert.AreEqual(orgModsTempDirectoryPath, pack.ModsTempDirectoryPath);
            Assert.AreEqual(orgModIconsDirectoryPath, pack.ModIconsDirectoryPath);

            mpe.TriggerBeginEdit();
            mpe.ObjectToEdit.Title = newTitle;
            mpe.ObjectToEdit.Version = newVersion;
            mpe.ObjectToEdit.Author = newAuthor;
            mpe.ObjectToEdit.Description = newDescription;
            mpe.ObjectToEdit.ImageSource = newImageSource;
            Assert.AreEqual(orgGuid.ToString(), pack.Guid.ToString());
            Assert.AreEqual(newTitle, pack.Title);
            Assert.AreEqual(newVersion, pack.Version);
            Assert.AreEqual(true, pack.IsVersionVisible);
            Assert.AreEqual(newAuthor, pack.Author);
            Assert.AreEqual(newDescription, pack.Description);
            Assert.AreEqual(newImageSource, pack.ImageSource);

            Assert.AreEqual(orgModPackDirectoryPath, pack.ModPackDirectoryPath);
            Assert.AreEqual(orgModsDirectoryPath, pack.ModsDirectoryPath);
            Assert.AreEqual(orgModsTempDirectoryPath, pack.ModsTempDirectoryPath);
            Assert.AreEqual(orgModIconsDirectoryPath, pack.ModIconsDirectoryPath);

            mpe.TriggerEndEdit();
            Assert.AreEqual(orgGuid.ToString(), pack.Guid.ToString());
            Assert.AreEqual(newTitle, pack.Title);
            Assert.AreEqual(newVersion, pack.Version);
            Assert.AreEqual(true, pack.IsVersionVisible);
            Assert.AreEqual(newAuthor, pack.Author);
            Assert.AreEqual(newDescription, pack.Description);
            Assert.AreEqual(newImageSource, pack.ImageSource);

            Assert.AreEqual(orgModPackDirectoryPath, pack.ModPackDirectoryPath);
            Assert.AreEqual(orgModsDirectoryPath, pack.ModsDirectoryPath);
            Assert.AreEqual(orgModsTempDirectoryPath, pack.ModsTempDirectoryPath);
            Assert.AreEqual(orgModIconsDirectoryPath, pack.ModIconsDirectoryPath);
        }
    }
}
