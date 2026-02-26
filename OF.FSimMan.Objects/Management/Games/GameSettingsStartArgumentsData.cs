using OF.Base.Objects;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OF.FSimMan.Management.Games
{
    [Table("GameStartArguments")]
    public class GameSettingsStartArgumentsData : DataObject<GameSettingsStartArguments>
    {
        #region Properties
        public bool SkipIntros { get; set; } = false;
        public bool DisableFrameLimit { get; set; } = false;
        public bool EnableCheats { get; set; } = false;


        public Guid GameSettingsId { get; set; }

        [ForeignKey(nameof(GameSettingsId))]
        [Required]
        public GameSettingsDataBase GameSettings { get; set; } = null!;
        #endregion

        public override GameSettingsStartArguments FromData()
        {
            return new GameSettingsStartArguments
            {
                Id = Id,
                _skipIntros = SkipIntros,
                _disableFrameLimit = DisableFrameLimit,
                _enableCheats = EnableCheats
            };
        }

        public override void ToData(GameSettingsStartArguments value)
        {
            Id = value.Id;
            SkipIntros = value._skipIntros;
            DisableFrameLimit = value._disableFrameLimit;
            EnableCheats = value._enableCheats;
        }
    }
}
