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

        

        public void UpdateTime(float currentTime, float maxTime)
        {
            TimeLabel.SetText(currentTime.ToString("F0") + "s");
        }

        public void UpdateScore(int currentScore)
        {
            ScoreLabel.SetText(currentScore.ToString());
        }

        public override void Show()
        {
            TimerGroup.SetActive(true);
            ScoreGroup.SetActive(true);
        }

        public override void Hide()
        {
            TimerGroup.SetActive(false);
            ScoreGroup.SetActive(false);
        }
    }
}