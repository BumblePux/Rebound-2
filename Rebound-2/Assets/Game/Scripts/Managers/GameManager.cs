using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Unlockables;
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

        public Unlockable SelectedShip { get; set; }


        protected override void Awake()
        {
            base.Awake();

#if UNITY_EDITOR
            RunDebugMethods();
#endif
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