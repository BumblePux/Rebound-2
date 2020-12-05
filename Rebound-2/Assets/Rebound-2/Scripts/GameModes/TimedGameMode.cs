using BumblePux.Rebound.Actors;
using BumblePux.Rebound.Actors.Interactables;
using BumblePux.Rebound.Leaderboards;
using BumblePux.Rebound.Managers;
using BumblePux.Rebound.UI;
using BumblePux.Rebound.Utils;
using System.Collections;
using UnityEngine;

namespace BumblePux.Rebound.GameModes
{
    public class TimedGameMode : ReboundGameMode
    {
        [Header("References/Prefabs")]
        public Popup PopupPrefab;

        [Header("Audio")]
        public AudioClip MusicClip;

        [Header("Game Mode Delay Settings")]
        public float StartGameDelay = 1f;

        [Header("Timer Settings")]
        public float MaxTime = 40f;
        public float TimeBonus = 3f;
        public float TimePenalty = 4f;

        [Header("Player Settings")]
        public float StartSpeed = 50f;
        public float MaxSpeed = 350f;
        public float SpeedIncrementOnStarHit = 5f;

        [Header("Planet Settings")]
        public int ScoreToEnableSecondPlanet = 15;
        public int ScoreToEnableThirdPlanet = 45;

        // Properties
        private float timer;
        public float Timer
        {
            get => timer;
            private set
            {
                timer = value;
                EventBus.OnTimerChanged?.Invoke(Timer);
            }
        }

        // References
        private AudioManager audioManager;
        private LeaderboardManager leaderboard;

        private PlanetsManager planetsManager;
        private Player player;
        private Animator animator;

        // Variables
        private float currentSpeed;
        private bool secondPlanetEnabled;
        private bool thirdPlanetEnabled;

        private int countdownStartHash;

        //---------------------------------------------------------------------------
        // Unity Start
        //---------------------------------------------------------------------------
        private void Start()
        {
            CurrentState = State.Loading;

            audioManager = GameUtils.GetAudioManager();
            leaderboard = GameUtils.GetLeaderboard();
            planetsManager = GameUtils.GetPlanetsManager();
            player = GameUtils.GetPlayer();
            animator = GetComponentInChildren<Animator>();

            countdownStartHash = Animator.StringToHash(GameConstants.COUNTDOWN_START);

            player.CurrentState = Player.State.Idle;
            currentSpeed = StartSpeed;
            Score = 0;            

            StartGameLoop();
        }

        //---------------------------------------------------------------------------
        // Game Loop
        //---------------------------------------------------------------------------
        protected override IEnumerator GameSetup()
        {
            audioManager.StopMusic(false);
            CurrentState = State.GameSetup;
            Timer = MaxTime;
            CountdownComplete = false;

            yield return null;

            player.SetStartPosition(planetsManager.GetRandomActivePlanet());
            player.Speed = currentSpeed;

            yield return new WaitForSeconds(StartGameDelay);

            animator.SetTrigger(countdownStartHash);

            while (!CountdownComplete)
                yield return null;
        }

        //--------------------------------------------------
        protected override IEnumerator GameRunning()
        {
            if (MusicClip)
                audioManager.PlayMusic(MusicClip);

            CurrentState = State.GameRunning;
            player.CurrentState = Player.State.Moving;

            while (CurrentState != State.GameOver)
            {
                Timer -= Time.deltaTime;
                if (Timer <= float.Epsilon)
                {
                    Timer = 0f;
                    CurrentState = State.GameOver;
                }

                yield return null;
            }
        }

        //--------------------------------------------------
        protected override IEnumerator GameOver()
        {
            CurrentState = State.GameOver;
            player.CurrentState = Player.State.Idle;

            leaderboard.ReportScore(LeaderboardBase.LeaderboardType.TimedMode, "Puxley", Score);

            yield return null;
        }


        //---------------------------------------------------------------------------
        // Game Events
        //---------------------------------------------------------------------------
        public override void StarHit(Star star)
        {
            // Increase score
            Score += star.PointValue;

            // Increase player speed and clamp
            currentSpeed += SpeedIncrementOnStarHit;
            currentSpeed = Mathf.Clamp(currentSpeed, StartSpeed, MaxSpeed);
            player.Speed = currentSpeed;

            // Increase remaining time and clamp
            Timer += TimeBonus;
            Timer = Mathf.Clamp(Timer, 0f, MaxTime);

            // Attempt to change player direction
            player.TryChangeDirection(true);

            // Attempt to enable second planet
            if (Score >= ScoreToEnableSecondPlanet && !secondPlanetEnabled)
            {
                planetsManager.NumPlanets++;
                secondPlanetEnabled = true;
            }

            // Attempt to enable third planet
            if (Score >= ScoreToEnableThirdPlanet && !thirdPlanetEnabled)
            {
                planetsManager.NumPlanets++;
                thirdPlanetEnabled = true;
            }

            // Show time gained popup
            SpawnPopup(player.transform.position, $"+{TimeBonus}s");
        }

        //--------------------------------------------------
        public override void StarMissed()
        {
            // Decrease remaining time
            Timer -= TimePenalty;

            // Show time lost popup
            SpawnPopup(player.transform.position, $"-{TimePenalty}s");
        }

        //---------------------------------------------------------------------------
        // Pause Handling Overrides
        //---------------------------------------------------------------------------
        protected override void Pause()
        {
            base.Pause();
            player.CurrentState = Player.State.Idle;
        }

        //--------------------------------------------------
        protected override void Resume()
        {
            base.Resume();
            player.CurrentState = Player.State.Moving;
        }

        //---------------------------------------------------------------------------
        // Popups
        //---------------------------------------------------------------------------
        private void SpawnPopup(Vector3 spawnPoint, string text)
        {
            if (PopupPrefab != null)
            {
                var popup = Instantiate(PopupPrefab, transform.position, Quaternion.identity);
                popup.SetStartPoint(spawnPoint);
                popup.SetText(text);
            }
        }
    }
}