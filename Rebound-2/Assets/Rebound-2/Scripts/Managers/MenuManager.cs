using BumblePux.Rebound.UI.Menus;
using BumblePux.Rebound.Utils;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.Managers
{
    public class MenuManager : MonoBehaviour
    {
        public enum MenuType { Settings, Customise, Home, Leaderboards, Achievements }

        [Header("References")]
        public GameObject MenuButtonContainer;
        public GameObject MenusContainer;

        [Header("Settings")]
        public MenuType CurrentMenu = MenuType.Home;
        public float SelectedButtonScale = 1.2f;
        public float ButtonEaseDuration = 0.5f;

        [Header("Audio")]
        public AudioClip MainMenuClip;
        public AudioClip ButtonPressedClip;
        
        private GameObject[] menuButtons;
        private MenuBase[] menus;

        private AudioManager audioManager;
        private bool menuLoaded;                    // Used to stop click sound playing when scene loads. Could be improved but works.

        //---------------------------------------------------------------------------
        private void Start()
        {
            audioManager = GameUtils.GetAudioManager();
            if (MainMenuClip)
                audioManager.PlayMusic(MainMenuClip);

            GetMenuButtons();
            GetMenus();
            OpenMenu(CurrentMenu);

            menuLoaded = true;
        }

        //--------------------------------------------------
        private void GetMenuButtons()
        {
            int childCount = MenuButtonContainer.transform.childCount;
            menuButtons = new GameObject[childCount];

            for (int i = 0; i < childCount; i++)
            {
                menuButtons[i] = MenuButtonContainer.transform.GetChild(i).gameObject;
            }
        }

        //--------------------------------------------------
        private void GetMenus()
        {
            int childCount = MenusContainer.transform.childCount;
            menus = new MenuBase[childCount];

            for (int i = 0; i < childCount; i++)
            {
                menus[i] = MenusContainer.transform.GetChild(i).GetComponent<MenuBase>();
            }
        }

        //--------------------------------------------------
        public void OpenMenu(int menuIndex)
        {
            StopAllCoroutines();

            CurrentMenu = (MenuType)menuIndex;

            for (int i = 0; i < menus.Length; i++)
            {
                if (menus[i].MenuType == CurrentMenu)
                    menus[i].Select();
                else
                    menus[i].Deselect();
            }

            if (ButtonPressedClip && menuLoaded)
                audioManager.PlaySfx(ButtonPressedClip);

            StartCoroutine(UpdateButtonScales());
        }

        //--------------------------------------------------
        public void OpenMenu(MenuBase menu)
        {
            OpenMenu((int)menu.MenuType);
        }

        //--------------------------------------------------
        public void OpenMenu(MenuType menuType)
        {
            OpenMenu((int)menuType);
        }

        //--------------------------------------------------
        private IEnumerator UpdateButtonScales()
        {
            float t = 0f;
            while (t <= 1f)
            {
                t += Time.deltaTime / ButtonEaseDuration;
                for (int i = 0; i < menuButtons.Length; i++)
                {
                    RectTransform buttonRectTransform = menuButtons[i].GetComponent<RectTransform>();

                    if (menus[i].MenuType == CurrentMenu)
                    {
                        buttonRectTransform.localScale = Vector3.Lerp(menuButtons[i].transform.localScale, Vector3.one * SelectedButtonScale, Mathf.SmoothStep(0f, 1f, t));
                    }
                    else
                    {
                        buttonRectTransform.localScale = Vector3.Lerp(menuButtons[i].transform.localScale, Vector3.one, Mathf.SmoothStep(0f, 1f, t));
                    }
                }

                yield return null;
            }
        }
    }
}