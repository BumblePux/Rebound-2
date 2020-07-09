using BumblePux.Rebound.GameModes;
using BumblePux.Tools.Singleton;
using UnityEngine;

namespace BumblePux.Rebound.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Debug")]
        [SerializeField] bool FindGameModeOnStart;

        public GameModeBase CurrentGameMode { get; set; }


        private void Start()
        {
            RunDebugMethods();
        }


#if UNITY_EDITOR

        private void RunDebugMethods()
        {

        }

        private void TryFindGameModeOnStart()
        {
            if (FindGameModeOnStart)
            {
                CurrentGameMode = FindObjectOfType<GameModeBase>();
                if (CurrentGameMode)
                {
                    Debug.Log($"{CurrentGameMode.GetType().Name} GameMode found!");
                }
                else
                {
                    Debug.Log("No GameMode found.");
                }
            }
        }

#endif
    }
}