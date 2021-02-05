using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine;
using BumblePux.Rebound2.Saving;

namespace BumblePux.Rebound2.Achievements
{
    public class AchievementsManager : MonoBehaviour
    {
        public static AchievementsManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public void Unlock(string achievementID)
        {
            Social.ReportProgress(achievementID, 100.0f, (success) =>
            {
                // handle success or failure
            });
        }

        public void Increment(string achievementID, int increment)
        {
            PlayGamesPlatform.Instance.IncrementAchievement(achievementID, increment, (success) =>
            {
                // handle success or failure
            });
        }

        public void TryUnlockWelcome()
        {
            Unlock(GPGSIds.achievement_welcome_to_rebound_2);
        }

        public void TryUnlockLifetimeCoins()
        {
            //if (SaveManager.Data.Stats.LifetimeCoins >= 1000000)
            //{
            //    Unlock(GPGSIds.achievement_millionaire);
            //}
        }

        public void TryUnlockBoughtFirstShip()
        {
            Unlock(GPGSIds.achievement_customiser);
        }

        public void TryUnlockCombo(int combo)
        {
            switch (combo)
            {
                case 25:
                    Unlock(GPGSIds.achievement_on_a_roll);
                    break;
                case 50:
                    Unlock(GPGSIds.achievement_combo_master);
                    break;
                default:
                    break;
            }
        }
    }
}