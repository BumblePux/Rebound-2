using BumblePux.Rebound.GameModes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.UI
{
    public class TimedGameModeUI : MonoBehaviour
    {
        [Header("Game Mode")]
        public TimedGameMode timedMode;

        [Header("Time UI Elements")]
        public TMP_Text TimeLabel;
        public Image TimeCircle;

        [Header("Score UI Elements")]
        public TMP_Text ScoreLabel;



        private void OnEnable()
        {
            timedMode.OnTimeChanged += UpdateTime;
            timedMode.OnScoreChanged += UpdateScore;
        }

        private void OnDisable()
        {
            timedMode.OnTimeChanged -= UpdateTime;
            timedMode.OnScoreChanged -= UpdateScore;
        }

        public void UpdateTime(float currentTime, float maxTime)
        {
            TimeLabel.SetText(currentTime.ToString("F0") + "s");

            float percent = currentTime / maxTime;
            TimeCircle.fillAmount = percent;
        }

        public void UpdateScore(int currentScore)
        {
            ScoreLabel.SetText(currentScore.ToString());
        }
    }
}