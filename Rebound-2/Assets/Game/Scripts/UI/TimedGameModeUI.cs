using BumblePux.Rebound.GameModes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.UI
{
    public class TimedGameModeUI : HUD
    {
        [Header("UI Groups")]
        public GameObject TimerGroup;
        public GameObject ScoreGroup;

        [Header("Time UI Elements")]
        public TMP_Text TimeLabel;
        public Image TimeCircle;

        [Header("Score UI Elements")]
        public TMP_Text ScoreLabel;

        private TimedGameMode timedMode;

        float lastTime = -1f;
        int lastScore = -1;

        private void Awake()
        {
            timedMode = GetGameMode() as TimedGameMode;
        }

        private void Update()
        {
            if (lastTime != timedMode.CurrentTime)
                UpdateTime(timedMode.CurrentTime, timedMode.MaxTime);

            if (lastScore != timedMode.CurrentScore)
                UpdateScore(timedMode.CurrentScore);
        }

        public void UpdateTime(float currentTime, float maxTime)
        {
            TimeLabel.SetText(currentTime.ToString("F0") + "s");

            float percent = currentTime / maxTime;
            TimeCircle.fillAmount = percent;

            lastTime = currentTime;
        }

        public void UpdateScore(int currentScore)
        {
            ScoreLabel.SetText(currentScore.ToString());

            lastScore = currentScore;
        }
    }
}