using OF.Base.Objects;
using System.Xml.Serialization;

namespace OliverFida.FSimMan.Config.ModPack
{
    public class ModData : ParentedDataObject<Mod>
    {
        public string Title = string.Empty;

        [XmlElement(IsNullable = true)]
        public string Version = string.Empty;

        public string Author = string.Empty;

        [XmlElement(IsNullable = true)]
        public string Description = string.Empty;

        [XmlElement(IsNullable = false)]
        public bool IsMultiplayerCompatible = false;

        [XmlElement(IsNullable = true)]
        public string? ImageSource = null;

        [XmlElement(IsNullable = false)]
        public string FileName = string.Empty;

        public override Mod FromData(object? parent)
        {
            Mod temp = new Mod((ModPack?)parent, Title, FileName)
            {
                _version = Version,
                _author = Author,
                _description = Description,
                _isMultiplayerCompatible = IsMultiplayerCompatible,
                _imageSource = ImageSource
            };

            return temp;
        }

        public override void ToData(Mod value)
        {
            Title = value.Title;
            Version = value.Version;
            Author = value.Author;
            Description = value.Description;
            IsMultiplayerCompatible = value.IsMultiplayerCompatible;
            ImageSource = value.ImageSource;
            FileName = value.FileName;
        }
    }
}
