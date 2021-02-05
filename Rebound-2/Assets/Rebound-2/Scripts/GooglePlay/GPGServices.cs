using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine;

namespace BumblePux.Rebound2.GooglePlay
{
    public static class GPGServices
    {
        private const bool DebugEnabled = true;

        public static void Initialize()
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
                .EnableSavedGames()
                .Build();

            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = DebugEnabled;
            PlayGamesPlatform.Activate();
        }

        public static void SignIn(Action<bool> callback)
        {
            PlayGamesPlatform.Instance.Authenticate(SignInInteractivity.CanPromptOnce, (result) =>
            {
                if (result == SignInStatus.Success)
                {
                    callback?.Invoke(true);
                }
                else
                {
                    callback?.Invoke(false);
                }
            });
        }

        public static void SignOut()
        {
            PlayGamesPlatform.Instance.SignOut();
        }

        public static void ShowLeaderboardsUI()
        {
            Social.ShowLeaderboardUI();
        }

        public static void ShowAchievementsUI()
        {
            Social.ShowAchievementsUI();
        }
    }
}