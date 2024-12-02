using OF.Base.Objects;
using System.Xml.Serialization;

namespace OF.FSimMan.Game
{
    [XmlType("Mod")]
    public class ModData : ParentedDataObject<Mod>
    {
        [XmlElement(IsNullable = false)]
        public string FileName = string.Empty;

        [XmlElement(IsNullable = false)]
        public string Title = string.Empty;

        public string? Version;

        public string? Author;

        public string? Description;

        [XmlElement(IsNullable = false)]
        public bool IsMultiplayerCompatible = false;

        public string? ImageSource;

        public override Mod FromData(object? parent)
        {
            return new Mod((ModPack)parent!, FileName)
            {
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
            FileName = value.FileName;
            Title = value.Title;
            Version = value.Version;
            Author = value.Author;
            Description = value.Description;
            IsMultiplayerCompatible = value.IsMultiplayerCompatible;
            ImageSource = value.ImageSource;
        }
    }
}
