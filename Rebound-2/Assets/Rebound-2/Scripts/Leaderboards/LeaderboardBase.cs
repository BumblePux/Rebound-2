using BumblePux.Rebound.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BumblePux.Rebound.Leaderboards
{
    public abstract class LeaderboardBase : MonoBehaviour
    {
        public enum LeaderboardType
        {
            TimedMode
        }

        protected HighScore[] highScores;

        //--------------------------------------------------------------------------------
        public abstract void ReportScore(LeaderboardType leaderboard, string userName, int score);
        public abstract void RequestScores(LeaderboardType leaderboard, UnityAction<bool, string> callback);
        public abstract HighScore[] GetScores();
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