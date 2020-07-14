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
        public event Action<float, float> OnTimeChanged;        

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

        private float currentTime;
        public float CurrentTime
        {
            get => currentTime;
            set
            {
                currentTime = value;
                OnTimeChanged?.Invoke(currentTime, MaxTime);
            }
        }        

        private float currentPlayerSpeed;

        private bool secondPlanetEnabled;
        private bool thirdPlanetEnabled;

        private PlayerMovement player;



        private void Start()
        {
            PlanetsManager.Instance.Initialize();
            TargetsManager.Instance.Initialize();
            player = Instantiate(PlayerPrefab, Vector3.zero, Quaternion.identity);

            currentPlayerSpeed = StartSpeed;
            CurrentScore = 0;

            StartGameLoop();
        }

        protected override IEnumerator GameInitialize()
        {
            Debug.Log("Game Initialize");

            IsGameOver = false;
            HasGameStarted = false;

            CurrentTime = MaxTime;
            player.Speed = currentPlayerSpeed;

            player.Initialize();
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

            yield return null;
        }

        public override void TargetHit(Target target)
        {
            player.TryChangeDirection(true);

            CurrentScore++;

            CurrentTime += TimeBonus;
            if (CurrentTime > MaxTime)
                CurrentTime = MaxTime;
            
            if (currentPlayerSpeed < MaxSpeed)
            {
                currentPlayerSpeed += SpeedIncrementOnTargetHit;
                if (currentPlayerSpeed > MaxSpeed)
                    currentPlayerSpeed = MaxSpeed;

                player.Speed = currentPlayerSpeed;
            }

            if (CurrentScore >= ScoreToEnableSecondPlanet && !secondPlanetEnabled)
            {
                PlanetsManager.Instance.PlanetCount++;
                secondPlanetEnabled = true;
            }

            if (CurrentScore >= ScoreToEnableThirdPlanet && !thirdPlanetEnabled && secondPlanetEnabled)
            {
                PlanetsManager.Instance.PlanetCount++;
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