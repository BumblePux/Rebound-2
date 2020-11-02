using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BumblePux.Rebound.UI
{
    public class GameOverMenu : MonoBehaviour
    {
        [Header("References")]
        public GameObject GameOverPanel;
        public TMP_Text SessionScoreText;

        [Header("Audio")]
        public AudioClip GameOverClip;

        private ReboundGameMode gameMode;
        private AudioManager audioManager;
        private LevelLoader levelLoader;

        //---------------------------------------------------------------------------
        public void GoToMainMenu()
        {
            levelLoader.LoadLevel(GameConstants.MAIN_MENU);
        }

        //--------------------------------------------------
        public void RetryGameMode()
        {
            levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

        //--------------------------------------------------
        public void WatchRewardedAd()
        {
            Debug.Log("Would show a rewarded ad now. Not yet implemented!");
        }

        //--------------------------------------------------
        private void Start()
        {
            gameMode = GameUtils.GetGameMode();
            audioManager = GameUtils.GetAudioManager();
            levelLoader = GameUtils.GetLevelLoader();
        }

        private void OnEnable()
        {
            EventBus.OnGameModeStateChanged += HandleGameModeChanged;
        }

        //--------------------------------------------------
        private void OnDisable()
        {
            EventBus.OnGameModeStateChanged -= HandleGameModeChanged;
        }

        //--------------------------------------------------
        private void HandleGameModeChanged(GameModeBase.State newState)
        {
            if (newState == GameModeBase.State.GameOver)
                ShowMenu();
            else
                HideMenu();
        }

        //--------------------------------------------------
        private void ShowMenu()
        {
            if (GameOverClip)
            {
                audioManager.PlayMusic(GameOverClip);
            }

            SessionScoreText.SetText(gameMode.Score.ToString());
            GameOverPanel.SetActive(true);
        }

        //--------------------------------------------------
        private void HideMenu()
        {
            GameOverPanel.SetActive(false);
        }
    }
}