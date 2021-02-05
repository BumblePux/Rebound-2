using BumblePux.Rebound2.Gameplay;
using TMPro;
using UnityEngine;

namespace BumblePux.Rebound2.UI.Gameplay
{
    public class TimerDisplay : MonoBehaviour
    {
        [Header("Required References")]
        public TMP_Text TimerText;
                

        public void UpdateTimerText(float remainingTime)
        {
            TimerText?.SetText(remainingTime.ToString("F2"));
        }
    }
}