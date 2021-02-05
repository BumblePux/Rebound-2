using UnityEngine;

namespace BumblePux.Rebound2.Utils
{
    public class PersistentObject : MonoBehaviour
    {
        private static PersistentObject instance;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}