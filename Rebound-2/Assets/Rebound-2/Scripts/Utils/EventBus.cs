using BumblePux.Rebound.GameModes;
using UnityEngine;
using UnityEngine.Events;

namespace BumblePux.Rebound.Utils
{
    public static class EventBus
    {
        // Game Mode Events
        public static UnityAction<GameModeBase.State> OnGameModeStateChanged;
        public static UnityAction<int> OnScoreChanged;
        public static UnityAction<float> OnTimerChanged;
    }
}