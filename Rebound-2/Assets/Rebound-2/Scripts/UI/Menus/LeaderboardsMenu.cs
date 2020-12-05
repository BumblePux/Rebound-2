using BumblePux.Rebound.Leaderboards;
using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BumblePux.Rebound.UI.Menus
{
    public class LeaderboardsMenu : MenuBase
    {
        [Header("References")]
        public GameObject LeaderboardContent;
        public Button RefreshButton;
        public LeaderboardEntry LeaderboardEntryPrefab;

        private LeaderboardManager leaderboard;

        //---------------------------------------------------------------------------
        private void Awake()
        {
            leaderboard = GameUtils.GetLeaderboard();
        }

        //--------------------------------------------------
        private void OnEnable()
        {
            if (gameObject.activeInHierarchy && leaderboard != null)
                RefreshLeaderboard();
        }

        //--------------------------------------------------
        public void RefreshLeaderboard()
        {
            TMP_Text refreshText = RefreshButton.GetComponentInChildren<TMP_Text>();
            refreshText.SetText("Refreshing");
            RefreshButton.interactable = false;

            leaderboard.RequestScores(LeaderboardBase.LeaderboardType.TimedMode, (success, response) =>
            {
                if (success)
                {
                    HighScore[] highScores = leaderboard.GetScores();

                    // Clear current leaderboard entries
                    foreach (Transform child in LeaderboardContent.transform)
                    {
                        Destroy(child.gameObject);
                    }

                    // Populate current leaderboard
                    for (int i = 0; i < highScores.Length; i++)
                    {
                        LeaderboardEntry entry = Instantiate(LeaderboardEntryPrefab, LeaderboardContent.transform, false);
                        entry.SetEntryText(i + 1, highScores[i].Name, highScores[i].Score);

                        // TODO: Manually enable Image and Text, as these are disabled for some reason. FIX!
                        Image[] images = entry.GetComponentsInChildren<Image>();
                        TMP_Text[] texts = entry.GetComponentsInChildren<TMP_Text>();
                        foreach (var image in images) image.enabled = true;
                        foreach (var text in texts) text.enabled = true;
                    }

                    refreshText.SetText("Refresh");
                    RefreshButton.interactable = true;
                }
                else
                {
                    refreshText.SetText("Refresh");
                    RefreshButton.interactable = true;
                }
            });
        }
    }
}