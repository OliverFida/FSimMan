using OF.Base.Client;
using OF.FSimMan.Game;
using OF.FSimMan.Management;

namespace OF.FSimMan.Client.Game
{
    public interface IGameClient : IClient
    {
        public FSimMan.Management.Game Game { get; }
        public ModPacks ModPacks { get; }
        public ModPack? SelectedModPack { get; set; }
        public bool IsGameRunning { get; }
        public GameState GameState { get; }

        public event EventHandler? GameStateChanged;

        public void RefreshModPacks(bool doControlBusyIndicator = true);
        public ModPack GetNewModPack();
        public void DeleteModPack(ModPack modPack);
        public void RunGame();
        public void StopGame();
        public void WaitForGameState(GameState gameState, bool isGameRunning);
    }
}
