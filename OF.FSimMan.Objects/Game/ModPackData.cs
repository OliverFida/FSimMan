using OF.Base.Objects;
using System.Xml.Serialization;

namespace OF.FSimMan.Game
{
    [XmlType("ModPack")]
    public class ModPackData : DataObject<ModPack>
    {
        [XmlElement(IsNullable = false)]
        public Guid Guid;

        [XmlElement(IsNullable = false)]
        public Management.Game Game;

        [XmlElement(IsNullable = false)]
        public string Title = string.Empty;

        public string? Version;

        public string? Author;

        public string? Description;

        public string? ImageSource = null;

        //[XmlArray(nameof(Mods), IsNullable = true)]
        //public ModData[] Mods = [];

        public override ModPack FromData()
        {
            ModPack temp = new ModPack
            {
                _guid = Guid,
                _game = Game,
                _title = Title,
                _version = Version,
                _author = Author,
                _description = Description,
                _imageSource = ImageSource,
            };

            //foreach (ModData mod in Mods)
            //{
            //    // OFDO: temp._mods.Add(mod.FromData(temp));
            //    temp._mods.Add(mod.FromData());
            //}

            return temp;
        }

        public override void ToData(ModPack value)
        {
            Guid = value._guid;
            Game = value._game;
            Title = value._title;
            Version = value._version;
            Author = value._author;
            Description = value._description;
            ImageSource = value._imageSource;

            //{
            //    List<ModData> temp = new List<ModData>();
            //    foreach (Mod mod in value.Mods)
            //    {
            //        ModData data = new ModData();
            //        data.ToData(mod);
            //        temp.Add(data);
            //    }
            //    Mods = temp.ToArray();
            //}
        }
    }
}
