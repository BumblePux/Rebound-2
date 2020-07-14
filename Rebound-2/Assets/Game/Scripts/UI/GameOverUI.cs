using BumblePux.Rebound.GameModes;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BumblePux.Rebound.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [Header("UI Elements")]
        public TMP_Text ScoreLabel;

        private GameModeBase gameMode;


        private void Awake()
        {
            gameMode = GameplayStatics.GetGameMode();
            gameMode.OnGameOverChanged += HandleOnGameOver;
        }

        private void OnEnable()
        {            
            ScoreLabel.SetText(gameMode.CurrentScore.ToString());
        }

        public void WatchAd()
        {
            Debug.Log("Would show a Rewarded Ad now.");
            gameMode.StartGameLoop();
        }

        public void ReloadGameMode()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }

        private void HandleOnGameOver(bool isGameOver)
        {
            if (!isGameOver)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }
        }

        private void OnDestroy()
        {
            gameMode.OnGameOverChanged -= HandleOnGameOver;
        }
    }
}