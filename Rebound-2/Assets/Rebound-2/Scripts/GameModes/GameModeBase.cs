using BumblePux.Rebound.Utils;
using System.Collections;
using UnityEngine;

namespace BumblePux.Rebound.GameModes
{
    public abstract class GameModeBase : MonoBehaviour
    {
        public enum State
        {
            Loading,
            GameSetup,
            GameRunning,
            GameOver,
            Paused
        }

        private State currentState;
        public State CurrentState
        {
            get => currentState;
            set
            {
                currentState = value;
                EventBus.OnGameModeStateChanged?.Invoke(currentState);
            }
        }

        //---------------------------------------------------------------------------
        // Game Loop
        //---------------------------------------------------------------------------
        public void StartGameLoop()
        {
            StartCoroutine(GameLoop());
        }

        //--------------------------------------------------
        private IEnumerator GameLoop()
        {
            yield return StartCoroutine(GameSetup());
            yield return StartCoroutine(GameRunning());
            yield return StartCoroutine(GameOver());
        }

        //--------------------------------------------------
        protected abstract IEnumerator GameSetup();
        protected abstract IEnumerator GameRunning();
        protected abstract IEnumerator GameOver();

        //---------------------------------------------------------------------------
        // Pause Handling
        //---------------------------------------------------------------------------
        public void TogglePause()
        {
            if (CurrentState != State.Paused)
                Pause();
            else
                Resume();
        }

        //--------------------------------------------------
        protected virtual void Pause()
        {
            CurrentState = State.Paused;
            Time.timeScale = 0f;
        }

        //--------------------------------------------------
        protected virtual void Resume()
        {
            CurrentState = State.GameRunning;
            Time.timeScale = 1f;
        }
    }
}