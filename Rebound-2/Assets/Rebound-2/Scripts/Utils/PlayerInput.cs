using UnityEngine;
using UnityEngine.EventSystems;

namespace BumblePux.Rebound.Utils
{
    public class PlayerInput : MonoBehaviour
    {
        public enum MouseButton { LeftClick, RightClick, MiddleMouse }

        [Header("PC Controls")]
        public KeyCode InteractKey = KeyCode.Space;
        public MouseButton InteractMouseButton = MouseButton.LeftClick;

        private EventSystem eventSystem;

        //---------------------------------------------------------------------------
        private void Start()
        {
            eventSystem = EventSystem.current;
        }

        //--------------------------------------------------
        public bool Interact()
        {
#if UNITY_STANDALONE || UNITY_EDITOR
            return GetPCInput();
#elif UNITY_ANDROID || UNITY_IOS
            return GetMobileInput();
#endif
        }

        //--------------------------------------------------
        private bool GetPCInput()
        {
            if (eventSystem.IsPointerOverGameObject())
                return false;

            return Input.GetKeyDown(InteractKey) || Input.GetMouseButtonDown((int)InteractMouseButton);
        }

        //--------------------------------------------------
        private bool GetMobileInput()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began && eventSystem.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return false;

                return Input.GetTouch(0).phase == TouchPhase.Began;
            }
            else
                return false;
        }
    }
}