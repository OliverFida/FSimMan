using OF.Base.Client;
using OF.FSimMan.Game;

namespace OF.FSimMan.Client.Game
{
    public interface IGameClient : IClient
    {
        public FSimMan.Management.Game Game { get; }
        public ModPacks ModPacks { get; }

        public void RefreshModPacks(bool doControlBusyIndicator = true);
        public ModPack GetNewModPack();
        public void DeleteModPack(ModPack modPack);
    }
}
