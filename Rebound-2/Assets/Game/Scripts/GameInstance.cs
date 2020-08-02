using BumblePux.Rebound.GameModes;
using BumblePux.Tools.Singleton;
using UnityEngine;

namespace BumblePux
{
    public class GameInstance : Singleton<GameInstance>
    {
        [Header("Current References")]
        [SerializeField] private GameModeBase currentGameMode = default;
        public GameModeBase CurrentGameMode { get => currentGameMode; set => currentGameMode = value; }
    }
}