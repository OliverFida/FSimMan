using OF.Base.Objects;
using System.Xml.Serialization;

namespace OF.FSimMan.Management.Games.Fs
{
    public class AppSettingsGameFsStartArgumentsData : DataObject<AppSettingsGameFsStartArguments>
    {
        [XmlElement(IsNullable = false)]
        public bool SkipIntros = false;

        [XmlElement(IsNullable = false)]
        public bool DisableFrameLimit = false;

        [XmlElement(IsNullable = false)]
        public bool EnableCheats = false;

        public override AppSettingsGameFsStartArguments FromData()
        {
            return new AppSettingsGameFsStartArguments
            {
                _skipIntros = SkipIntros,
                _disableFrameLimit = DisableFrameLimit,
                _enableCheats = EnableCheats
            };
        }

        public override void ToData(AppSettingsGameFsStartArguments value)
        {
            SkipIntros = value._skipIntros;
            DisableFrameLimit = value._disableFrameLimit;
            EnableCheats = value._enableCheats;
        }
    }
}
