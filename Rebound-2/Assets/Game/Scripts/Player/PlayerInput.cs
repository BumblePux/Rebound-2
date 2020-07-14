using UnityEngine;
using UnityEngine.EventSystems;

namespace BumblePux.Rebound.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public static bool InputEnabled { get; set; }

        private EventSystem eventSystem;


        private void Awake()
        {
            eventSystem = EventSystem.current;
        }

        public bool Interact()
        {
            if (!InputEnabled)
                return false;

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
            else
                return Input.GetMouseButtonDown(0);
        }

        private bool GetMobileInput()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && eventSystem.IsPointerOverGameObject(touch.fingerId))
                {
                    return false;
                }
                else
                {
                    return touch.phase == TouchPhase.Began;
                }
            }
            else
            {
                return false;
            }
        }
    }
}