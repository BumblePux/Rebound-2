using UnityEngine;

namespace BumblePux.Rebound.Utils
{
    public class PersistentObject : MonoBehaviour
    {
        //---------------------------------------------------------------------------
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}