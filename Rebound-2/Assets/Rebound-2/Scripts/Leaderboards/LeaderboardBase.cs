using BumblePux.Rebound.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BumblePux.Rebound.Leaderboards
{
    public abstract class LeaderboardBase : Singleton<LeaderboardBase>
    {
        public enum LeaderboardType
        {
            TimedMode
        }

        protected List<HighScore> highScores = new List<HighScore>();

        //--------------------------------------------------------------------------------
        public abstract void ReportScore(LeaderboardType leaderboard, string userName, int score);
        public abstract void RequestScores(LeaderboardType leaderboard, UnityAction<bool, string> callback);
        public abstract List<HighScore> GetScores();
    }

    //--------------------------------------------------------------------------------
    public struct HighScore
    {
        public readonly string Name;
        public readonly int Score;

        public HighScore(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}