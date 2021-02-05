using BumblePux.Rebound2.Achievements;
using BumblePux.Rebound2.Advertisements;
using BumblePux.Rebound2.Audio;
using BumblePux.Rebound2.GameModes;
using BumblePux.Rebound2.Gameplay;
using BumblePux.Rebound2.Leaderboards;
using BumblePux.Rebound2.Managers;
using BumblePux.Rebound2.Saving;
using BumblePux.Rebound2.Utils;
using TMPro;
using UnityEngine;

namespace BumblePux.Rebound2.UI.Menus
{
    public class GameOverMenu : MenuBase
    {
        [Header("References")]
        public TimedGameMode GameMode;

        [Header("Internal References")]
        public GameObject GameOverPanel;
        public GameObject AdButtons;
        public TMP_Text ScoreText;
        public TMP_Text CoinsText;
        public TMP_Text ComboText;

        private Counter score;

        private void Awake()
        {
            score = GameMode.Score;

            GameMode.OnGameSetup += Hide;
            GameMode.OnGameOver += Show;
        }

        public override void Show()
        {
            AudioManager.Instance.Play("GameOver");
            GameOverPanel.SetActive(true);
            ScoreText.SetText(score.Value.ToString());
            CoinsText.SetText(GameMode.SessionCoinCount.ToString());
            ComboText.SetText(GameMode.SessionHighestCombo.ToString());

            LeaderboardManager.Instance.ReportScore(GPGSIds.leaderboard_timed_mode, GameMode.Score.Value);
            
            AchievementsManager.Instance.TryUnlockWelcome();
            AchievementsManager.Instance.TryUnlockLifetimeCoins();

            if (GameMode.Score.Value == 0)
                AdButtons.SetActive(false);

            //SaveManager.Save();
        }

        public override void Hide()
        {
            GameOverPanel.SetActive(false);
        }

        public void LoadMainMenu()
        {
            SceneLoader.LoadScene(GameConstants.Scenes.SCENE_MAIN_MENU);
        }

        public void RetryGameMode()
        {
            SceneLoader.LoadScene(GameConstants.Scenes.SCENE_GAMEMODE_TIMED);
        }

        public void WatchExtraLifeRewardedAd()
        {
            UnityAdsManager.Instance.PlayRewardedVideoAd((success) =>
            {
                if (success)
                {
                    AdButtons.SetActive(false);
                    GameMode.StartGameLoop();
                }
            });
        }

        public void WatchDoubleCoinsRewardedAd()
        {
            UnityAdsManager.Instance.PlayRewardedVideoAd((success) =>
            {
                AdButtons.SetActive(false);
                //SaveManager.Data.Coins += GameMode.SessionCoinCount;
                //SaveManager.Data.Stats.LifetimeCoins += GameMode.SessionCoinCount;
                GameMode.SessionCoinCount *= 2;
                Show();
            });
        }

        private void OnDestroy()
        {
            GameMode.OnGameSetup -= Hide;
            GameMode.OnGameOver -= Show;
        }
    }
}