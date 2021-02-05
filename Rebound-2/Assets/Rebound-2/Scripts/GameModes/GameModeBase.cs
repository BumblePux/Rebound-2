using BumblePux.Rebound2.Actors.Interactables;
using System;
using System.Collections;
using UnityEngine;

namespace BumblePux.Rebound2.GameModes
{
    public abstract class GameModeBase : MonoBehaviour
    {
        public Action OnGameSetup;
        public Action OnGameStarted;
        public Action OnGameOver;
        public Action OnPaused;
        public Action OnResumed;

        [Header("Game Mode State")]
        public bool IsPaused;

        public void StartGameLoop()
        {
            StartCoroutine(GameLoop());
        }

        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(GameSetup());
            yield return StartCoroutine(GameRunning());
            yield return StartCoroutine(GameOver());
        }

        protected abstract IEnumerator GameSetup();
        protected abstract IEnumerator GameRunning();
        protected abstract IEnumerator GameOver();

        // Game specific methods.
        protected abstract void StarHit(Star star);
        protected abstract void StarMissed();

        public void TogglePause()
        {
            if (!IsPaused)
                Pause();
            else
                Resume();
        }

        protected virtual void Pause()
        {
            IsPaused = true;
            Time.timeScale = 0f;
            OnPaused?.Invoke();
        }

        protected virtual void Resume()
        {
            IsPaused = false;
            Time.timeScale = 1f;
            OnResumed?.Invoke();
        }
    }
}