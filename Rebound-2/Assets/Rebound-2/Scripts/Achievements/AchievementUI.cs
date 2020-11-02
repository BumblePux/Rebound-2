using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.Achievements
{
    public class AchievementUI : MonoBehaviour
    {
        [Header("References")]
        public TMP_Text NameText;
        public TMP_Text DescriptionText;
        public Image IconImage;

		//--------------------------------------------------------------------------------
        public void SetAchievementInfo(string name, string description, Sprite icon)
        {
            NameText.SetText(name);
            DescriptionText.SetText(description);
            IconImage.sprite = icon;
        }
    }
}