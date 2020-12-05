using UnityEngine;

namespace BumblePux.Rebound.Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        private static bool isApplicationQuitting;
        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                if (isApplicationQuitting)
                {
#if UNITY_EDITOR
                    Debug.LogWarning($"[{typeof(T).Name}] Instance already destroyed on application quit. Won't create again. Returning null.");
#endif
                    return null;
                }

                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<T>();
                        if (instance == null)
                        {
#if UNITY_EDITOR
                            Debug.LogError($"[{typeof(T).Name}] No instance found. Creating one...");
#endif
                            GameObject singleton = new GameObject();
                            instance = singleton.AddComponent<T>();
                            singleton.name = typeof(T).Name;

                            DontDestroyOnLoad(singleton);
                        }
                    }

                    return instance;
                }
            }
        }

        //--------------------------------------------------------------------------------
        private static bool IsDontDestroyOnLoad()
        {
            if (instance == null)
                return false;

            // Object exists independent of Scene lifecycle. Assume that means it has DontDestroyOnLoad set.
            if ((instance.gameObject.hideFlags & HideFlags.DontSave) == HideFlags.DontSave)
                return true;

            return false;
        }

        //--------------------------------------------------------------------------------
        protected virtual void OnDestroy()
        {
            if (IsDontDestroyOnLoad())
                isApplicationQuitting = true;
        }

        //--------------------------------------------------------------------------------
        //protected virtual void Awake()
        //{
        //    if (instance != null && instance != this as T)
        //    {
        //        Destroy(gameObject);
        //    }
        //    else
        //    {
        //        instance = this as T;
        //        DontDestroyOnLoad(gameObject);
        //    }
        //}
    }
}