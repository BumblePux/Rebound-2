using BumblePux.Rebound.Interactables;
using BumblePux.Rebound.Managers;
using System;
using System.Collections;
using UnityEngine;

namespace BumblePux.Rebound.GameModes
{
    [DefaultExecutionOrder(-100)]
    public abstract class GameModeBase : Actor
    {
        [Header("Status Flags")]
        [SerializeField] bool isGameOver;
        public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

        public bool HasGameStarted;

        [Header("Base Settings")]
        public float PlayerOffset = 5f;

        [Header("UI")]
        [SerializeField] protected GameObject GameOverUIPrefab;

        [Header("Required References")]
        public PlanetsManager PlanetsManager;

        public int CurrentScore { get; protected set; }


        #region UNITY_METHODS

        private void Awake()
        {
            GetGameInstance().CurrentGameMode = this;
        }

        private void OnDestroy()
        {
#if !UNITY_EDITOR
            GetGameInstance().CurrentGameMode = null;
#endif
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