using BumblePux.Rebound2.Audio;
using BumblePux.Rebound2.GameModes;
using BumblePux.Rebound2.Managers;
using BumblePux.Rebound2.Utils;
using UnityEngine;

namespace BumblePux.Rebound2.UI.Menus
{
    public class PauseMenu : MenuBase
    {
        [Header("References")]
        public GameModeBase GameMode;

        [Header("Internal References")]
        public GameObject PausePanel;

        private void Awake()
        {
            GameMode.OnGameSetup += Hide;
            GameMode.OnPaused += Show;
            GameMode.OnResumed += Hide;
        }

        public void CloseMenu()
        {
            GameMode.TogglePause();
        }

        public override void Show()
        {
            AudioManager.Instance.Play("Paused");
            PausePanel.SetActive(true);
        }

        public override void Hide()
        {
            PausePanel.SetActive(false);
        }

        public void LoadMainMenu()
        {
            GameMode.TogglePause();
            SceneLoader.LoadScene(GameConstants.Scenes.SCENE_MAIN_MENU);
        }

        public void RetryGameMode()
        {
            GameMode.TogglePause();
            SceneLoader.LoadScene(GameConstants.Scenes.SCENE_GAMEMODE_TIMED);
        }

        private void OnDestroy()
        {
            GameMode.OnGameSetup -= Hide;
            GameMode.OnPaused -= Show;
            GameMode.OnResumed -= Hide;
        }
    }
}