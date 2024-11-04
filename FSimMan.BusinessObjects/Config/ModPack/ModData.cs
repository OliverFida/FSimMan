using OliverFida.Base;
using System.Xml.Serialization;

namespace OliverFida.FSimMan.Config.ModPack
{
    public class ModData : DataObjectBase<Mod>
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
        public string ImageSource = string.Empty;

        [XmlElement(IsNullable = false)]
        public string FileName = string.Empty;

        public override Mod FromData()
        {
            Mod temp = new Mod(Title, FileName)
            {
                _version = Version,
                _author = Author,
                _description = Description,
                _isMultiplayerCompatible = IsMultiplayerCompatible
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
            FileName = value.FileName;
        }
    }
}
