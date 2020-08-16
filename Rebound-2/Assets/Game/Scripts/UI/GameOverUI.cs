using BumblePux.Rebound.GameModes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BumblePux.Rebound.UI
{
    public class GameOverUI : HUD
    {
        [Header("Groups")]
        [SerializeField] GameObject GameOverUIRoot = default;

        [Header("UI Elements")]
        public TMP_Text ScoreLabel;


        public void WatchAd()
        {
            Debug.Log("Would show a Rewarded Ad now.");
            GetGameMode().StartGameLoop();
        }

        public void ReloadGameMode()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        public override void Show()
        {
            GameOverUIRoot.SetActive(true);
            ScoreLabel.SetText(GetGameMode().CurrentScore.ToString());
        }

        public override void Hide()
        {
            GameOverUIRoot.SetActive(false);
        }
    }
}