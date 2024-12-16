using OF.FSimMan.Game;
using System.Diagnostics.CodeAnalysis;

namespace OF.FSimMan.Tests.Objects.Game
{
    [TestClass]
    [TestCategory("ci")]
    [ExcludeFromCodeCoverage]
    public class ModTests
    {
        [TestMethod]
        public void Mod_Self()
        {
            // Unnamed mod
            ModPack pack = new ModPack(Management.Game.FarmingSim22);

            ModData data = new ModData();
            Mod mod = data.FromData();

            Assert.AreEqual(string.Empty, mod.FileName);
            Assert.AreEqual(string.Empty, mod.Title);
            Assert.AreEqual(null, mod.Version);
            Assert.AreEqual(null, mod.Author);
            Assert.AreEqual(null, mod.Description);
            Assert.AreEqual(false, mod.IsMultiplayerCompatible);
            Assert.AreEqual(null, mod.ImageSource);
            Assert.AreEqual(null, mod.FullImageSource);

            string fileName = "testFileName.zip";
            mod = new Mod(pack, fileName);

            Assert.AreEqual(fileName, mod.FileName);
            Assert.AreEqual("Unnamed mod", mod.Title);
            Assert.AreEqual(null, mod.Version);
            Assert.AreEqual(null, mod.Author);
            Assert.AreEqual(null, mod.Description);
            Assert.AreEqual(false, mod.IsMultiplayerCompatible);
            Assert.AreEqual(null, mod.ImageSource);
            Assert.AreEqual(null, mod.FullImageSource);

            // Correct mod
            string title = "Test Mod";
            string version = "0.2.1";
            string author = "Test Author";
            string description = "Wonderful description";
            bool isMpCompatible = true;
            string imageSource = "Some/Image.source";
            string fullImageSource = Path.Combine(pack.ModIconsDirectoryPath, imageSource);

            data = new ModData()
            {
                FileName = fileName,
                Title = title,
                Version = version,
                Author = author,
                Description = description,
                IsMultiplayerCompatible = isMpCompatible,
                ImageSource = imageSource
            };

            mod = data.FromData(pack);

            Assert.AreEqual(fileName, mod.FileName);
            Assert.AreEqual(title, mod.Title);
            Assert.AreEqual(version, mod.Version);
            Assert.AreEqual(author, mod.Author);
            Assert.AreEqual(description, mod.Description);
            Assert.AreEqual(isMpCompatible, mod.IsMultiplayerCompatible);
            Assert.AreEqual(imageSource, mod.ImageSource);
            Assert.AreEqual(fullImageSource, mod.FullImageSource);

            // Empty image source
            data.ImageSource = string.Empty;
            mod = data.FromData(pack);

            Assert.AreEqual(null, mod.FullImageSource);
        }
    }
}
