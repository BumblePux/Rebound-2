using BumblePux.Rebound.Interactables;
using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Player;
using BumblePux.Rebound.UI;
using System;
using System.Collections;
using UnityEngine;

namespace BumblePux.Rebound.GameModes
{
    public class TimedGameMode : GameModeBase
    {
        public TargetsManager TargetsManager;

        [Header("Required Prefabs")]
        public PlayerMovement PlayerPrefab;

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

        public float CurrentTime { get; private set; }

        private float currentPlayerSpeed;

        private bool secondPlanetEnabled;
        private bool thirdPlanetEnabled;

        private PlayerMovement player;
        private GameObject gameOverUI;



        private void Start()
        {
            gameOverUI = Instantiate(GameOverUIPrefab, Vector3.zero, Quaternion.identity);            

            PlanetsManager.Initialize();
            TargetsManager.Initialize();
            player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);
            player.Initialize();

            currentPlayerSpeed = StartSpeed;
            CurrentScore = 0;

            StartGameLoop();
        }

        protected override IEnumerator GameInitialize()
        {
            Debug.Log("Game Initialize");

            // Reset game state
            IsGameOver = false;
            HasGameStarted = false;

            // Disable UI
            gameOverUI.SetActive(false);

            // Set starting parameters.
            // Player speed equals currentPlayerSpeed rather than StartSpeed so as to retain the previous session speed if an Ad was successfully watched.
            CurrentTime = MaxTime;
            player.Speed = currentPlayerSpeed;
            player.SetNewRandomTarget();
            
            PlayerInput.InputEnabled = false;

            yield return null;
        }

        protected override IEnumerator GameInProgress()
        {
            Debug.Log("Game in progress");

            HasGameStarted = true;
            PlayerInput.InputEnabled = true;

            while (!IsGameOver)
            {
                CurrentTime -= Time.deltaTime;

                if (CurrentTime <= float.Epsilon)
                {
                    CurrentTime = 0f;
                    IsGameOver = true;
                }

                yield return null;
            }            
        }

        protected override IEnumerator GameOver()
        {
            Debug.Log("Game Over");

            PlayerInput.InputEnabled = false;

            // Show game over UI
            gameOverUI.SetActive(true);

            yield return null;
        }

        public override void TargetHit(Target target)
        {
            player.TryChangeDirection(true);

            CurrentScore++;

            // Increment time and clamp
            CurrentTime += TimeBonus;
            if (CurrentTime > MaxTime)
                CurrentTime = MaxTime;
            
            // Increase player speed and clamp
            if (currentPlayerSpeed < MaxSpeed)
            {
                currentPlayerSpeed += SpeedIncrementOnTargetHit;
                if (currentPlayerSpeed > MaxSpeed)
                    currentPlayerSpeed = MaxSpeed;

                player.Speed = currentPlayerSpeed;
            }

            // Try to enable 2nd planet
            if (CurrentScore >= ScoreToEnableSecondPlanet && !secondPlanetEnabled)
            {
                PlanetsManager.PlanetCount++;
                secondPlanetEnabled = true;
            }

            // Try to enable 3rd planet
            if (CurrentScore >= ScoreToEnableThirdPlanet && !thirdPlanetEnabled && secondPlanetEnabled)
            {
                PlanetsManager.PlanetCount++;
                thirdPlanetEnabled = true;
            }
        }

        public override void TargetMissed()
        {
            Debug.Log("Target missed");

            CurrentTime -= TimePenalty;
        }
    }
}