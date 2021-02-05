using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.UI.Menus
{
    public abstract class MenuBase : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}