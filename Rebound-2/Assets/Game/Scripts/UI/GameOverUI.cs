using BumblePux.Rebound.GameModes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BumblePux.Rebound.UI
{
    public class GameOverUI : HUD
    {
        [Header("UI Elements")]
        public TMP_Text ScoreLabel;



        private void OnEnable()
        {            
            ScoreLabel.SetText(GetGameMode().CurrentScore.ToString());
        }

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
    }
}