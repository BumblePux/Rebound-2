using BumblePux.Rebound2.Achievements;
using BumblePux.Rebound2.Actors.Interactables;
using BumblePux.Rebound2.Actors.Player;
using BumblePux.Rebound2.Audio;
using BumblePux.Rebound2.Camera;
using BumblePux.Rebound2.Gameplay;
using BumblePux.Rebound2.Gameplay.Managers;
using BumblePux.Rebound2.Saving;
using System.Collections;
using UnityEngine;

namespace BumblePux.Rebound2.GameModes
{
    public class TimedGameMode : GameModeBase
    {
        [Header("Required References")]
        public PlanetsManager PlanetsManager;
        public ShipController Ship;
        public Counter Score;
        public Counter Combo;
        public Timer Timer;

        [Header("Game Mode Settings")]
        public float GameStartDelay = 3f;
        public float GameOverDelay = 1f;

        [Header("Player Settings")]
        public float PlayerDistanceFromPlanet = 5f;
        public float PlayerStartSpeed = 50f;
        public float PlayerMaxSpeed = 350f;
        public float PlayerSpeedIncrementOnStarHit = 5f;

        [Header("Timer Settings")]
        public float TimerMaxtime = 40f;
        public float TimerHitBonus = 3f;
        public float TimerMissPenalty = 4f;

        [Header("Planet Spawn Settings")]
        public int ScoreToEnableSecondPlanet = 15;
        public int ScoreToEnableThirdPlanet = 40;

        private float playerCurrentSpeed;
        private bool secondPlanetEnabled;
        private bool thirdPlanetEnabled;

        public int SessionCoinCount { get; set; }
        public int SessionHighestCombo { get; private set; }


        private void OnEnable()
        {
            Star.OnStarHit += StarHit;
            Ship.OnInteractMissed += StarMissed;
        }

        private void OnDisable()
        {
            Star.OnStarHit -= StarHit;
            Ship.OnInteractMissed -= StarMissed;
        }

        private void Start()
        {
            PlanetsManager.Initialize(PlayerDistanceFromPlanet);
            playerCurrentSpeed = PlayerStartSpeed;
            Score.Value = 0;
            SessionCoinCount = 0;
            SessionHighestCombo = 0;
            StartGameLoop();
        }

        protected override IEnumerator GameSetup()
        {
            AudioManager.Instance.StopMusic();
            OnGameSetup?.Invoke();

            Ship.Initialize(
                PlanetsManager.GetRandomActivePlanet(),
                PlayerDistanceFromPlanet,
                playerCurrentSpeed);

            Ship.CanMove = false;
            Timer.Begin(TimerMaxtime);
            Combo.Value = 0;
            yield return new WaitForSeconds(GameStartDelay);
        }

        protected override IEnumerator GameRunning()
        {
            AudioManager.Instance.Play("TimedGameModeTheme");
            OnGameStarted?.Invoke();

            Ship.CanMove = true;
            Timer.Resume();

            while (!Timer.HasElapsed)
            {
                // Do nothing. Waiting for timer to reach zero.
                yield return null;
            }            
        }

        protected override IEnumerator GameOver()
        {
            AudioManager.Instance.StopMusic();
            UpdateStats();

            Ship.CanMove = false;
            yield return new WaitForSeconds(GameOverDelay);
            OnGameOver?.Invoke();
        }

        private void UpdateStats()
        {
            //SaveManager.Data.Coins += SessionCoinCount;
            //SaveManager.Data.Stats.LifetimeCoins += SessionCoinCount;

            //if (SessionHighestCombo > SaveManager.Data.Stats.TimedModeHighestCombo)
            //{
            //    SaveManager.Data.Stats.TimedModeHighestCombo = SessionHighestCombo;
            //}
        }

        protected override void StarHit(Star star)
        {
            Score.Value += star.PointValue;

            Combo.Value++;
            if (Combo.Value > SessionHighestCombo)
            {
                SessionHighestCombo = Combo.Value;
            }

            Timer.IncreaseRemainingTime(TimerHitBonus);

            playerCurrentSpeed += PlayerSpeedIncrementOnStarHit;
            playerCurrentSpeed = Mathf.Clamp(playerCurrentSpeed, PlayerStartSpeed, PlayerMaxSpeed);
            Ship.SetMoveSpeed(playerCurrentSpeed);
            Ship.TryChangeDirection(true);

            TryEnableNewPlanet();

            SessionCoinCount += Combo.Value;

            AchievementsManager.Instance.TryUnlockCombo(Combo.Value);

            CinemachineShake.Instance.Shake(10f, 0.2f);
        }

        private void TryEnableNewPlanet()
        {
            if (Score.Value >= ScoreToEnableSecondPlanet && !secondPlanetEnabled)
            {
                PlanetsManager.NumPlanets++;
                secondPlanetEnabled = true;
            }

            if (Score.Value >= ScoreToEnableThirdPlanet && !thirdPlanetEnabled)
            {
                PlanetsManager.NumPlanets++;
                thirdPlanetEnabled = true;
            }
        }

        protected override void StarMissed()
        {
            Combo.Value = 0;
            Timer.DecreaseRemainingTime(TimerMissPenalty);
            AudioManager.Instance.Play("StarMiss");
            CinemachineShake.Instance.Shake(25f, 0.25f);
        }

        protected override void Resume()
        {
            base.Resume();
            AudioManager.Instance.Play("TimedGameModeTheme");
        }
    }
}