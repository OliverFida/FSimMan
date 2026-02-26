using OF.Base.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace OF.FSimMan.Game
{
    [Table("Mods")]
    [XmlType("Mod")]
    public class ModData : ParentedDataObject<Mod>
    {
        #region Properties
        [Required]
        [XmlElement(IsNullable = false)]
        public string FileName { get; set; } = string.Empty;

        [Required]
        [XmlElement(IsNullable = false)]
        public string Title { get; set; } = string.Empty;

        public string? Version { get; set; }

        public string? Author { get; set; }

        public string? Description { get; set; }

        [Required]
        [XmlElement(IsNullable = false)]
        public bool IsMultiplayerCompatible { get; set; } = false;

        public string? ImageSource { get; set; }


        public Guid ModPackId { get; set; }

        [ForeignKey(nameof(ModPackId))]
        [Required]
        public ModPackData ModPack { get; set; } = null!;
        #endregion

        #region Methods PUBLIC
        public override Mod FromData(object? parent)
        {
            return new Mod((ModPack)parent!, FileName)
            {
                Id = Id,
                _title = Title,
                _version = Version,
                _author = Author,
                _description = Description,
                _isMultiplayerCompatible = IsMultiplayerCompatible,
                _imageSource = ImageSource,
            };
        }

        public override void ToData(Mod value)
        {
            Id = value.Id;
            FileName = value.FileName;
            Title = value.Title;
            Version = value.Version;
            Author = value.Author;
            Description = value.Description;
            IsMultiplayerCompatible = value.IsMultiplayerCompatible;
            ImageSource = value.ImageSource;
        }
        #endregion
    }
}
