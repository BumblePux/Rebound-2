using BumblePux.Rebound2.Utils;
using UnityEngine;

namespace BumblePux.Rebound2.Managers
{
    public class GameManager : Singleton<GameManager>
    {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void CreateInstance()
        {
            Debug.Log("[GameManager] CreateInstance() started...");

            var manager = Instance?.gameObject;

            if (Instance == null)
            {
                manager = new GameObject("GameManager");
                manager.AddComponent<GameManager>();

                Debug.Log("[GameManager] CreateInstance() Instance null. Created new Instance.");
            }

            if (Instance.IsPersistent)
                DontDestroyOnLoad(manager);

            Debug.Log("[GameManager] CreateInstance() finished.");
        }
    }
}