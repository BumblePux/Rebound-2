using BumblePux.Rebound.Interactables;
using System.Collections;
using UnityEngine;

namespace BumblePux.Rebound.GameModes
{
    public abstract class GameModeBase : MonoBehaviour
    {
        [Header("Status Flags")]
        public bool IsGameOver;
        public bool HasGameStarted;

        [Header("Base Settings")]
        public float PlayerOffset = 5f;


        protected void StartGameLoop()
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


        public abstract void TargetHit(Target target);
        public abstract void TargetMissed();
    }
}