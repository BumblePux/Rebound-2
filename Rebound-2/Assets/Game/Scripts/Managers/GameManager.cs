using BumblePux.Rebound.GameModes;
using UnityEngine;

namespace BumblePux.Rebound.Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameModeBase CurrentGameMode { get; set; }
    }
}