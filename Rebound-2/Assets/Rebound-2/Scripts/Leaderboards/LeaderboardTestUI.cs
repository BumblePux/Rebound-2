using BumblePux.Rebound.Utils;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.Leaderboards
{
    public class LeaderboardTestUI : MonoBehaviour
    {
        [Header("References")]
        public GameObject LeaderboardContent;
        public LeaderboardEntry LeaderboardEntryPrefab;

        private LeaderboardBase leaderboard;

        //--------------------------------------------------------------------------------
        public void Start()
        {
            leaderboard = GameUtils.GetLeaderboard();

            leaderboard.RequestScores(LeaderboardBase.LeaderboardType.TimedMode, (success, response) =>
            {
                if (success)
                {
                    List<HighScore> highScores = leaderboard.GetScores();

                    // Clear leaderboard
                    foreach (Transform child in LeaderboardContent.transform)
                    {
                        Destroy(child.gameObject);
                    }

                    for (int i = 0; i < highScores.Count; i++)
                    {
                        LeaderboardEntry entry = Instantiate(LeaderboardEntryPrefab, LeaderboardContent.transform);
                        entry.SetEntryText(i + 1, highScores[i].Name, highScores[i].Score);
                    }
                }
            });
        }
    }
}