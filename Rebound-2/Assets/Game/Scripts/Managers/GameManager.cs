using BumblePux.Rebound.GameModes;
using BumblePux.Tools.Singleton;
using UnityEngine;

namespace BumblePux.Rebound.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public GameModeBase CurrentGameMode { get; set; }
    }
}