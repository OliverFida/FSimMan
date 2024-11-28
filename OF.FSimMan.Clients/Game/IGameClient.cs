using OF.Base.Client;
using OF.FSimMan.Game;

namespace OF.FSimMan.Client.Game
{
    public interface IGameClient : IClient
    {
        public FSimMan.Management.Game Game { get; }
        public ModPacks ModPacks { get; }

        public void RefreshModPacks(bool doControlBusyIndicator = true);
        public void CreateNewModPack();
        public void DeleteModPack(ModPack modPack);
    }
}
