using BumblePux.Rebound2.Gameplay;
using TMPro;
using UnityEngine;

namespace BumblePux.Rebound2.UI.Gameplay
{
    public class ScoreDisplay : MonoBehaviour
    {
        [Header("Required References")]
        public TMP_Text ScoreText;

        
        public void UpdateScoreText(int newScore)
        {
            ScoreText?.SetText(newScore.ToString());
        }
    }
}