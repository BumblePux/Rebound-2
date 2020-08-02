using UnityEngine;

namespace BumblePux.Tools.Singleton
{
    public abstract class Singleton<T> : Actor where T : Singleton<T>
    {
        [Header("Singleton Settiings")]
        [SerializeField] bool isPersistent = true;

        private static bool shuttingDown = false;

        private static T instance;

        public static T Instance
        {
            get
            {
                if (shuttingDown)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"{typeof(T).Name} (Singleton) already destroyed. Returning null.");
#endif
                    return null;
                }

                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
#if UNITY_EDITOR
                        Debug.LogWarning($"{typeof(T).Name} instance not found in the scene. Creating one now...");
#endif
                        var singleton = new GameObject();
                        instance = singleton.AddComponent<T>();
                        singleton.name = typeof(T).Name + " (Singleton)";
                    }
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance != null && instance != this as T)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                instance = this as T;

                if (isPersistent)
                {
                    DontDestroyOnLoad(gameObject);
                }
            }
        }

        protected virtual void OnApplicationQuit()
        {
            shuttingDown = true;
        }

        protected virtual void OnDestroy()
        {
            shuttingDown = true;
        }
    }
}