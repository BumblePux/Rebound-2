﻿using BumblePux.Rebound.Leaderboards;
using BumblePux.Rebound.Utils;
using UnityEngine;
using UnityEngine.Events;
using static BumblePux.Rebound.Leaderboards.LeaderboardBase;

namespace BumblePux.Rebound.Managers
{
    public class LeaderboardManager : Singleton<LeaderboardManager>
    {
        public LeaderboardBase platform;

        //--------------------------------------------------------------------------------
        private void Awake()
        {
            // TODO: Remove Android support here. It's here for device testing only as Google Play isn't implemented yet.
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_ANDROID
            platform = gameObject.AddComponent<DreamloLeaderboard>();
#endif
        }

        //--------------------------------------------------------------------------------
        public void ReportScore(LeaderboardType leaderboard, string userName, int score)
        {
            platform.ReportScore(leaderboard, userName, score);
        }

        //--------------------------------------------------------------------------------
        public void RequestScores(LeaderboardType leaderboard, UnityAction<bool, string> callback)
        {
            platform.RequestScores(leaderboard, callback);
        }

        //--------------------------------------------------------------------------------
        public HighScore[] GetScores()
        {
            return platform.GetScores();
        }
    }
}