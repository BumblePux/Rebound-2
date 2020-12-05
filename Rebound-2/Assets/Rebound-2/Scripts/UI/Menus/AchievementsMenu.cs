using BumblePux.Rebound.Achievements;
using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Utils;
using UnityEngine;

namespace BumblePux.Rebound.UI.Menus
{
    public class AchievementsMenu : MenuBase
    {
        [Header("References")]
        public GameObject ScrollViewContent;
        public AchievementListItemUI AchievementListItemPrefab;

        private AchievementsManager achievements;

        //---------------------------------------------------------------------------
        private void Awake()
        {
            achievements = GameUtils.GetAchievements();
        }

        //---------------------------------------------------------------------------
        private void OnEnable()
        {
            if (gameObject.activeInHierarchy && achievements != null)
                RefreshAchievementsList();
        }

        //--------------------------------------------------
        private void RefreshAchievementsList()
        {
            // Remove current children
            if (ScrollViewContent.transform.childCount > 0)
            {
                foreach (Transform child in ScrollViewContent.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            achievements.RequestAchievements((success, response) =>
            {
                if (success)
                {
                    Achievement[] achievementsArray = achievements.GetAchievements();

                    // Populate achievementd list
                    for (int i = 0; i < achievementsArray.Length; i++)
                    {
                        AchievementListItemUI item = Instantiate(AchievementListItemPrefab, ScrollViewContent.transform, false);
                        item.SetAchievementInfo(achievementsArray[i].Title, achievementsArray[i].Description, achievementsArray[i].Icon);

                        // TODO: Manually enable Image and Text, as these are disabled for some reason. FIX!
                        //Image[] images = entry.GetComponentsInChildren<Image>();
                        //TMP_Text[] texts = entry.GetComponentsInChildren<TMP_Text>();
                        //foreach (var image in images) image.enabled = true;
                        //foreach (var text in texts) text.enabled = true;
                    }
                }
            });
        }
    }
}