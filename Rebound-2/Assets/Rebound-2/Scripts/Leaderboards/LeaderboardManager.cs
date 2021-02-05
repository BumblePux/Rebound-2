using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine;

namespace BumblePux.Rebound2.Leaderboards
{
    public class LeaderboardManager : MonoBehaviour
    {
        public static LeaderboardManager Instance { get; private set; }

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

        public void ReportScore(string leaderboard, int score)
        {
            Social.ReportScore(score, leaderboard, (success) =>
            {
                // handle success or failure
            });
        }
    }
}