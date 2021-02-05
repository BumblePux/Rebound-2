using UnityEngine;
using UnityEngine.EventSystems;

namespace BumblePux.Rebound2.Input
{
    public class PlayerInput : MonoBehaviour
    {
        private EventSystem eventSystem;

        private void Start()
        {
            eventSystem = EventSystem.current;
        }

        public bool Interact()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            return GetPCInput();
#elif UNITY_ANDROID || UNITY_IOS
            return GetMobileInput();
#endif
        }

        private bool GetPCInput()
        {
            if (eventSystem.IsPointerOverGameObject())
                return false;

            return UnityEngine.Input.GetKeyDown(KeyCode.Space) || UnityEngine.Input.GetMouseButtonDown(0);
        }

        private bool GetMobileInput()
        {
            if (UnityEngine.Input.touchCount > 0)
            {
                if (UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began && eventSystem.IsPointerOverGameObject(UnityEngine.Input.GetTouch(0).fingerId))
                    return false;

                return UnityEngine.Input.GetTouch(0).phase == TouchPhase.Began;
            }
            else
                return false;
        }
    }
}