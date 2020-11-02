using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BumblePux.Rebound.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [Header("References")]
        public GameObject PausePanel;
        public Button PauseButton;

        private ReboundGameMode gameMode;
        private LevelLoader levelLoader;

        //---------------------------------------------------------------------------
        public void GoToMainMenu()
        {
            gameMode.TogglePause();
            levelLoader.LoadLevel(GameConstants.MAIN_MENU);
        }

        //--------------------------------------------------
        public void RetryGameMode()
        {
            gameMode.TogglePause();
            levelLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex);
        }

        //--------------------------------------------------
        public void PauseGame()
        {
            gameMode.TogglePause();
            ToggleMenu();
        }

        //--------------------------------------------------
        private void Start()
        {
            gameMode = GameUtils.GetGameMode();
            levelLoader = GameUtils.GetLevelLoader();
            ToggleMenu();
        }

        //--------------------------------------------------
        private void OnEnable()
        {
            EventBus.OnGameModeStateChanged += HandleGameModeChanged;
        }

        //---------------------------------------------------------------------------
        private void OnDisable()
        {
            EventBus.OnGameModeStateChanged -= HandleGameModeChanged;
        }

        //--------------------------------------------------
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
                PauseGame();
        }

        //--------------------------------------------------
        private void HandleGameModeChanged(GameModeBase.State newState)
        {
            if (newState == GameModeBase.State.GameOver || newState == GameModeBase.State.GameSetup)
                PauseButton.gameObject.SetActive(false);
            else
                PauseButton.gameObject.SetActive(true);
        }

        //--------------------------------------------------
        private void ShowMenu()
        {
            PausePanel.SetActive(true);
        }

        //--------------------------------------------------
        private void HideMenu()
        {
            PausePanel.SetActive(false);
        }

        //--------------------------------------------------
        private void ToggleMenu()
        {
            if (gameMode.CurrentState == GameModeBase.State.Paused)
                ShowMenu();
            else
                HideMenu();
        }
    }
}