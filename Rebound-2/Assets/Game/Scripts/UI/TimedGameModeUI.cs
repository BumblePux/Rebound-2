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

        [Header("UI Groups")]
        public GameObject TimerGroup;
        public GameObject ScoreGroup;

        [Header("Time UI Elements")]
        public TMP_Text TimeLabel;
        public Image TimeCircle;

        [Header("Score UI Elements")]
        public TMP_Text ScoreLabel;



        private void OnEnable()
        {
            timedMode.OnGameOverChanged += HandleOnGameOver;
            timedMode.OnTimeChanged += UpdateTime;
            timedMode.OnScoreChanged += UpdateScore;
        }

        private void OnDisable()
        {
            timedMode.OnGameOverChanged -= HandleOnGameOver;
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

        public void HandleOnGameOver(bool isGameOver)
        {
            if (!isGameOver)
            {
                TimerGroup.SetActive(true);
                ScoreGroup.SetActive(true);
            }
            else
            {
                TimerGroup.SetActive(false);
                ScoreGroup.SetActive(false);
            }
        }
    }
}