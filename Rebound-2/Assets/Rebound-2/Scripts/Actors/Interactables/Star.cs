using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Utils;
using UnityEngine;

namespace BumblePux.Rebound.Actors.Interactables
{
    public class Star : MonoBehaviour, IInteractable
    {
        [Header("Settings")]
        public int PointValue = 1;
        public int NumSpawnPositions = 6;

        [Header("Effects")]
        public GameObject HitFxPrefab;

        [Header("Audio")]
        public AudioClip HitClip;

        private ReboundGameMode gameMode;
        private AudioManager audioManager;
        private Animator animator;

        private Transform planet;
        private Transform spriteHolder;
        private float offsetFromPlanet;

        //---------------------------------------------------------------------------
        private void Start()
        {
            animator = GetComponentInChildren<Animator>();

            audioManager = GameUtils.GetAudioManager();
            gameMode = GameUtils.GetGameMode();
            offsetFromPlanet = gameMode.PlayerDistanceFromPlanet;

            spriteHolder = animator.transform;
            planet = transform.parent;

            transform.position = planet.position;
            spriteHolder.localPosition = new Vector3(offsetFromPlanet, 0f, 0f);

            ChangePosition();
        }

        //---------------------------------------------------------------------------
        public void Interact()
        {
            gameMode.StarHit(this);

            if (HitFxPrefab)
            {
                Instantiate(HitFxPrefab, spriteHolder.position, Quaternion.identity);
            }

            if (HitClip)
            {
                audioManager.PlaySfx(HitClip);
            }

            ChangePosition();
        }

        //---------------------------------------------------------------------------
        private void ChangePosition()
        {
            int newPosition;

            do
            {
                int angle = 360 / NumSpawnPositions;
                int multiplier = Random.Range(0, NumSpawnPositions);

                newPosition = angle * multiplier;
            }
            while (newPosition == (int)transform.eulerAngles.z);

            Quaternion newRotation = Quaternion.Euler(0f, 0f, newPosition);
            transform.rotation = newRotation;

            animator.Play("Appear", -1, 0f);
        }
    }
}