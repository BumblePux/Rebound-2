using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Managers;

namespace BumblePux.Rebound
{
    public static class GameplayStatics
    {
        public static GameModeBase GetGameMode()
        {
            return GameManager.Instance.CurrentGameMode;
        }
    }
}