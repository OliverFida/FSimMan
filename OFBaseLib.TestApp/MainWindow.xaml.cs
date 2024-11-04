using OFBaseLib.TestApp.Objects;
using System.Diagnostics;
using System.Windows;

namespace OFBaseLib.TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Author author = new Author("Oliver Fida");
            Author newAuthor = new Author("Andreas Kiehtreiber");
            Mod mod = new Mod("Test Mod");
            Mod newMod = new Mod("Zweiter Mod");
            ModPack modPack = new ModPack("Test Modpack", author);
            modPack.Mods.Add(mod);
            Debug.WriteLine("");

            modPack.BeginEdit();
            bool modified = modPack.IsModified;
            Debug.WriteLine("");

            //modPack.Name += "1";
            //modPack.Author = newAuthor;
            modPack.Mods.Add(newMod);
            modified = modPack.IsModified;
            Debug.WriteLine("");

            modPack.CancelEdit();
            modified = modPack.IsModified;
            Debug.WriteLine("");

            modPack.BeginEdit();
            //modPack.Name += "2";
            //modPack.Author = newAuthor;
            modPack.Mods.Add(newMod);
            modified = modPack.IsModified;
            Debug.WriteLine("");

            modPack.EndEdit();
            modified = modPack.IsModified;
            Debug.WriteLine("");

            modPack.BeginEdit();
            //modPack.Author.Name = "Mario Ableidinger";
            modPack.Mods[1].Name = "Neuer Mod";
            modified = modPack.IsModified;
            Debug.WriteLine("");

            modPack.CancelEdit();
            modified = modPack.IsModified;
            Debug.WriteLine("");

            Close();
        }
    }
}