using CLS.Core.Objects;

namespace OF.FSimMan.Management.Games
{
    public interface IGameSettingsData : IDataObject
    {
        public GameSettingsStartArgumentsData StartArguments { get; set; }
    }
}
