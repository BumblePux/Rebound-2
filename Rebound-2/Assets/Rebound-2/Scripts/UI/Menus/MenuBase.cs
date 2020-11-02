using BumblePux.Rebound.Managers;
using UnityEngine;

namespace BumblePux.Rebound.UI.Menus
{
    public abstract class MenuBase : MonoBehaviour
    {
        [Header("Settings")]
        public MenuManager.MenuType MenuType;

        //---------------------------------------------------------------------------
        public virtual void Select()
        {
            gameObject.SetActive(true);
        }

        //--------------------------------------------------
        public virtual void Deselect()
        {
            gameObject.SetActive(false);
        }
    }
}