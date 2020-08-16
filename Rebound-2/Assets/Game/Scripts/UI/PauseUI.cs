using UnityEngine;
using UnityEngine.SceneManagement;

namespace BumblePux.Rebound.UI
{
    public class PauseUI : HUD
    {
        [Header("UI References")]
        [SerializeField] GameObject PauseButton = default;
        [SerializeField] GameObject PauseMenu = default;


        public void OnPausePressed()
        {
            GetGameMode().TogglePause();
        }

        public void ReloadGameMode()
        {
            OnPausePressed();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMainMenu()
        {
            OnPausePressed();
            SceneManager.LoadScene("MainMenu");
        }

        public void ResumePlay()
        {
            OnPausePressed();
        }

        public override void Show()
        {
            PauseButton.SetActive(false);
            PauseMenu.SetActive(true);
        }

        public override void Hide()
        {
            PauseButton.SetActive(true);
            PauseMenu.SetActive(false);
        }

        public void HideAll()
        {
            PauseButton.SetActive(false);
            PauseMenu.SetActive(false);
        }
    }
}