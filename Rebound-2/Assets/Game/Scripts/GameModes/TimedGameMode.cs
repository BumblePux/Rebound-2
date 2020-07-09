using System.Collections;
using UnityEngine;

namespace BumblePux.Rebound.GameModes
{
    public class TimedGameMode : GameModeBase
    {
        [Header("Timed Mode Base Settings")]
        public float CoyoteTime = 0.2f;

        [Header("Countdown Timer Settings")]
        public float MaxTime = 30f;
        public float TimeBonus = 2f;
        public float TimePenalty = 4f;

        [Header("Player Speed Settings")]
        public float StartSpeed = 50f;
        public float MaxSpeed = 400f;
        public float SpeedIncrementOnTargetHit = 5f;

        [Header("Planet Enable Settings")]
        public int ScoreToEnableSecondPlanet = 15;
        public int ScoreToEnableThirdPlanet = 40;

        private float currentTime;
        private float currentPlayerSpeed;
        private int currentScore;

        private bool secondPlanetEnabled;
        private bool thirdPlanetEnabled;



        private void Start()
        {
            StartGameLoop();
        }

        protected override IEnumerator GameInitialize()
        {
            Debug.Log("Game Initialize");

            yield return null;
        }

        protected override IEnumerator GameInProgress()
        {
            Debug.Log("Game in progress");

            yield return null;
        }

        protected override IEnumerator GameOver()
        {
            Debug.Log("Game Over");

            yield return null;
        }

        public override void TargetHit()
        {
            Debug.Log("Target hit");
        }

        public override void TargetMissed()
        {
            Debug.Log("Target missed");
        }
    }
}