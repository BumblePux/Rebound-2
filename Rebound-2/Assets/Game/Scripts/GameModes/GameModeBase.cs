using BumblePux.Rebound.Interactables;
using System;
using System.Collections;
using UnityEngine;

namespace BumblePux.Rebound.GameModes
{
    [DefaultExecutionOrder(-101)]
    public abstract class GameModeBase : Actor
    {
        public event Action<bool> OnGameOverChanged;
        public event Action<int> OnScoreChanged;

        [Header("Status Flags")]
        [SerializeField] bool isGameOver;
        public bool IsGameOver
        {
            get => isGameOver;
            set
            {
                isGameOver = value;
                OnGameOverChanged?.Invoke(isGameOver);
            }
        }

        public bool HasGameStarted;

        [Header("Base Settings")]
        public float PlayerOffset = 5f;

        private int currentScore;
        public int CurrentScore
        {
            get => currentScore;
            set
            {
                currentScore = value;
                OnScoreChanged?.Invoke(currentScore);
            }
        }


        #region UNITY_METHODS

        private void Awake()
        {
            GetGameInstance().CurrentGameMode = this;
        }

        private void OnDestroy()
        {
            GetGameInstance().CurrentGameMode = null;
        }

        #endregion


        #region GAME_LOOP

        public void StartGameLoop()
        {
            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(GameInitialize());
            yield return StartCoroutine(GameInProgress());
            yield return StartCoroutine(GameOver());
        }

        protected abstract IEnumerator GameInitialize();
        protected abstract IEnumerator GameInProgress();
        protected abstract IEnumerator GameOver();

        #endregion


        #region TARGET_METHODS

        public abstract void TargetHit(Target target);
        public abstract void TargetMissed();

        #endregion
    }
}