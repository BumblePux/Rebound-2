using TMPro;
using UnityEngine;

namespace BumblePux.Rebound.Leaderboards
{
    public class LeaderboardEntry : MonoBehaviour
    {
        [Header("References")]
        public TMP_Text LeaderboardPositionText;
        public TMP_Text UserNameText;
        public TMP_Text HighScoreText;

        //--------------------------------------------------------------------------------
        public void SetEntryText(int position, string userName, int score)
        {
            LeaderboardPositionText.SetText(position.ToString());
            UserNameText.SetText(userName);
            HighScoreText.SetText(score.ToString());
        }
    }
}