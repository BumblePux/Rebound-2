using BumblePux.Rebound.GameModes;
using BumblePux.Tools.Singleton;
using UnityEngine;

namespace BumblePux.Rebound.Managers
{
    [DefaultExecutionOrder(-100)]
    public class GameManager : Singleton<GameManager>
    {
        [Header("Debug")]
        [SerializeField] bool FindGameModeOnStart = default;

        public GameModeBase CurrentGameMode { get; set; }


        protected override void Awake()
        {
            base.Awake();

            RunDebugMethods();
        }


#if UNITY_EDITOR

        private void RunDebugMethods()
        {
            TryFindGameModeOnStart();
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