using UnityEngine;

namespace BumblePux.Rebound.SimpleBehaviours
{
    public class SimpleRotator2D : MonoBehaviour
    {
        [Header("Settings")]
        public float Speed = 5f;

        //--------------------------------------------------------------------------------
        private void Update()
        {
            transform.Rotate(Vector3.forward, Speed * Time.deltaTime);
        }
    }
}