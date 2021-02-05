using BumblePux.Rebound2.GameModes;
using BumblePux.Rebound2.UI.Gameplay;
using UnityEngine;

namespace BumblePux.Rebound2.UI.HUDs
{
    public class TimedGameModeHUD : MonoBehaviour
    {
        [Header("References")]
        public TimedGameMode TimedMode;

        [Header("Internal References")]
        public ScoreDisplay Score;
        public ComboDisplay Combo;
        public TimerDisplay Timer;
        public GameObject PauseButton;
        public GameObject BlockingPanel;

        private void Awake()
        {
            TimedMode.Score.OnValueChanged += Score.UpdateScoreText;
            TimedMode.Combo.OnValueChanged += Combo.UpdateComboText;
            TimedMode.Timer.OnRemainingTimeChanged += Timer.UpdateTimerText;

            TimedMode.OnGameSetup += SetupHUD;
            TimedMode.OnGameStarted += DisableBlockingPanel;
            TimedMode.OnPaused += HideHUD;
            TimedMode.OnResumed += ShowHUD;
            TimedMode.OnGameOver += HideHUD;
        }

        private void SetupHUD()
        {
            Score.gameObject.SetActive(true);
            Combo.gameObject.SetActive(true);
            Timer.gameObject.SetActive(true);
            PauseButton.SetActive(true);
            BlockingPanel.SetActive(true);
        }

        private void ShowHUD()
        {
            Score.gameObject.SetActive(true);
            Combo.gameObject.SetActive(true);
            Timer.gameObject.SetActive(true);
            PauseButton.SetActive(true);

            Combo.UpdateComboText(TimedMode.Combo.Value);
        }

        private void HideHUD()
        {
            Score.gameObject.SetActive(false);
            Combo.gameObject.SetActive(false);
            Timer.gameObject.SetActive(false);
            PauseButton.SetActive(false);
            BlockingPanel.SetActive(false);
        }

        private void DisableBlockingPanel()
        {
            BlockingPanel.SetActive(false);
        }

        private void OnDestroy()
        {
            TimedMode.Score.OnValueChanged -= Score.UpdateScoreText;
            TimedMode.Combo.OnValueChanged -= Combo.UpdateComboText;
            TimedMode.Timer.OnRemainingTimeChanged -= Timer.UpdateTimerText;

            TimedMode.OnGameSetup -= SetupHUD;
            TimedMode.OnGameStarted -= DisableBlockingPanel;
            TimedMode.OnPaused -= HideHUD;
            TimedMode.OnResumed -= ShowHUD;
            TimedMode.OnGameOver -= HideHUD;
        }
    }
}