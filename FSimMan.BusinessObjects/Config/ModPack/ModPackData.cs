using OF.Base.Objects;
using System.Xml.Serialization;

namespace OliverFida.FSimMan.Config.ModPack
{
    [XmlRoot("ModPack")]
    public class ModPackData : DataObject<ModPack>
    {
        [XmlElement(IsNullable = false)]
        public FsEdition FsEdition;

        [XmlElement(IsNullable = false)]
        public string Title = string.Empty;

        [XmlElement(IsNullable = false)]
        public string Author = "unknown";

        [XmlElement(IsNullable = true)]
        public string? Version = null;

        [XmlElement(IsNullable = true)]
        public string? ImageSource = null;

        [XmlElement(IsNullable = false)]
        public Guid Key;

        [XmlElement(IsNullable = true)]
        public string Description = string.Empty;

        [XmlArray(nameof(Mods), IsNullable = true)]
        [XmlArrayItem("Mod")]
        public ModData[] Mods = [];

        public override ModPack FromData()
        {
            ModPack temp = new ModPack(Title, Author, FsEdition)
            {
                _version = Version,
                _imageSource = ImageSource,
                _key = Key,
                _description = Description,
                _mods = new EditableObservableCollection<Mod>()
            };
            foreach (ModData mod in Mods)
            {
                temp._mods.Add(mod.FromData(temp));
            }

            return temp;
        }

        public override void ToData(ModPack value)
        {
            FsEdition = value._fsEdition;
            Title = value.Title;
            Author = value.Author;
            Version = value.Version;
            ImageSource = value.ImageSource;
            Description = value.Description;
            Key = value.Key;

            {
                List<ModData> temp = new List<ModData>();
                foreach (Mod mod in value.Mods)
                {
                    ModData data = new ModData();
                    data.ToData(mod);
                    temp.Add(data);
                }
                Mods = temp.ToArray();
            }
        }
    }
}
