using BumblePux.Rebound.Actors.Interactables;
using BumblePux.Rebound.Utils;
using UnityEngine;

namespace BumblePux.Rebound.GameModes
{
    public abstract class ReboundGameMode : GameModeBase
    {
        [Header("Rebound Mode Settings")]
        public float PlayerDistanceFromPlanet = 5f;

        private int score;
        public int Score
        {
            get => score;
            protected set
            {
                score = value;
                EventBus.OnScoreChanged?.Invoke(score);
            }
        }

        public bool CountdownComplete { get; set; }

        //---------------------------------------------------------------------------
        // Rebound Game Events
        //---------------------------------------------------------------------------
        public abstract void StarHit(Star star);
        public abstract void StarMissed();
    }
}