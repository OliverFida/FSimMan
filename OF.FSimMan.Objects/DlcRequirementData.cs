using OF.Base.Objects;
using OF.FSimMan.Game;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace OF.FSimMan
{
    [Table("DlcRequirements")]
    [XmlType("DlcRequirement")]
    public class DlcRequirementData : ParentedDataObject<DlcRequirement>
    {
        #region Properties
        [Required]
        [XmlElement(IsNullable = false)]
        public string FileName { get; set; } = string.Empty;


        public int ModPackId { get; set; }

        [ForeignKey(nameof(ModPackId))]
        [Required]
        public ModPackData ModPack { get; set; } = null!;
        #endregion

        #region Methods PUBLIC
        public override DlcRequirement FromData(object? parent)
        {
            return new DlcRequirement((ModPack)parent!, FileName)
            {
                Id = Id,
                _fileName = FileName,
            };
        }

        public override void ToData(DlcRequirement value)
        {
            Id = value.Id;
            FileName = value.FileName;
        }
        #endregion
    }
}
