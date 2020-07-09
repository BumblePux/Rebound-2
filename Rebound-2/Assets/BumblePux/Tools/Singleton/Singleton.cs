using UnityEngine;

namespace BumblePux.Tools.Singleton
{
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        [Header("Singleton Settiings")]
        [SerializeField] bool isPersistent = false;
        public bool DestroyOnSceneChange = false;

        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        Debug.LogWarning($"[{typeof(T).Name}] Instance called but not found. Is {typeof(T).Name} present in the scene?");
                        return null;
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

        protected virtual void OnDisable()
        {
            if (DestroyOnSceneChange)
            {
                Destroy(gameObject);
            }
        }
    }
}