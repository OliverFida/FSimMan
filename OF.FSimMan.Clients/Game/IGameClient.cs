using OF.Base.Client;
using OF.FSimMan.Game;

namespace OF.FSimMan.Client.Game
{
    public interface IGameClient : IClient
    {
        public FSimMan.Management.Game Game { get; }
        public ModPacks ModPacks { get; }
        public ModPack? SelectedModPack { get; set; }

        public void RefreshModPacks(bool doControlBusyIndicator = true);
        public ModPack GetNewModPack();
        public void DeleteModPack(ModPack modPack);
        public void RunGame();
        //public void StopGame();
        public void ExportModPack(ModPack modPack, string filePath);
        public bool GetModPackExists(string filePath);
        public void ImportModPack(string filePath, bool importAsNew);
    }
}
