using OF.Base.Objects;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace OF.FSimMan.Game
{
    [Table("ModPacks")]
    [XmlType("ModPack")]
    public class ModPackData : DataObject<ModPack>
    {
        [XmlElement(IsNullable = false)]
        public Management.Game Game { get; set; }

        [Required]
        [XmlElement(IsNullable = false)]
        public Guid Guid { get; set; } = Guid.NewGuid();

        [Required]
        [XmlElement(IsNullable = false)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [XmlElement(IsNullable = false)]
        public string Version { get; set; } = string.Empty;
        [XmlElement(IsNullable = false)]
        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        [XmlElement(IsNullable = false)]
        public string Description { get; set; } = string.Empty;

        public string? ImageSource { get; set; } = null;

        [Required]
        [XmlArray(nameof(Mods), IsNullable = true)]
        public List<ModData> Mods { get; set; } = new List<ModData>();

        [Required]
        [XmlArray(nameof(Dlcs), IsNullable = true)]
        public List<DlcRequirementData> Dlcs { get; set; } = new List<DlcRequirementData>();

        public override ModPack FromData()
        {
            ModPack temp = new ModPack
            {
                Id = Id,
                _guid = Guid,
                _game = Game,
                _title = Title,
                _version = Version,
                _author = Author,
                _description = Description,
                _imageSource = ImageSource,
            };

            foreach (ModData mod in Mods)
            {
                temp._mods.Add(mod.FromData(temp));
            }

            foreach (DlcRequirementData dlc in Dlcs)
            {
                temp._dlcs.Add(dlc.FromData(temp));
            }

            return temp;
        }

        public override void ToData(ModPack value)
        {
            Id = value.Id;
            Guid = value._guid;
            Game = value._game;
            Title = value._title;
            Version = value._version;
            Author = value._author;
            Description = value._description;
            ImageSource = value._imageSource;

            {
                ConcurrentBag<ModData> temp = new ConcurrentBag<ModData>();
                Parallel.ForEach(value.Mods, mod =>
                {
                    ModData data = new ModData();
                    data.ToData(mod);
                    temp.Add(data);
                });
                Mods = temp.ToList();
            }

            {
                ConcurrentBag<DlcRequirementData> temp = new ConcurrentBag<DlcRequirementData>();
                Parallel.ForEach(value.Dlcs, dlc =>
                {
                    DlcRequirementData data = new DlcRequirementData();
                    data.ToData(dlc);
                    temp.Add(data);
                });
                Dlcs = temp.ToList();
            }
        }
    }
}
