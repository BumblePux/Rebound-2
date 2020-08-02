using BumblePux.Rebound.GameModes;
using UnityEngine;

namespace BumblePux
{
    public class Actor : MonoBehaviour
    {
        public GameInstance GetGameInstance()
        {
            return GameInstance.Instance;
        }

        public GameModeBase GetGameMode()
        {
            return GameInstance.Instance.CurrentGameMode;
        }
    }
}