using BumblePux.Rebound2.Audio;
using BumblePux.Rebound2.Gameplay.Managers;
using BumblePux.Rebound2.Saving;
using System;
using UnityEngine;

namespace BumblePux.Rebound2.Actors.Interactables
{
    public class Star : MonoBehaviour, IInteractable
    {
        public static event Action<Star> OnStarHit;

        [Header("Required References")]
        public Transform SpriteHolder;
        public Transform Sprite;
        public Animator Animator;

        [Header("Settings")]
        public int PointValue = 1;
        public int NumSpawnPositions = 6;

        [Header("Effects Settings")]
        public GameObject HitFxPrefab;

        private PlanetsManager planetsManager;
        private Transform spriteTransform;

        private float distanceFromPlanet;

        private void Awake()
        {
            planetsManager = GetComponentInParent<PlanetsManager>();
            distanceFromPlanet = planetsManager.DistanceBetweenPlanets / 2f;

            spriteTransform = SpriteHolder.GetComponentInChildren<SpriteRenderer>().transform;
            spriteTransform.localPosition = new Vector3(distanceFromPlanet, 0f, 0f);

            ChangePosition();
        }

        public void Interact()
        {
            AudioManager.Instance.Play("StarPop");

            if (HitFxPrefab) Instantiate(HitFxPrefab, Sprite.position, Quaternion.identity);

            ChangePosition();

            OnStarHit?.Invoke(this);
        }

        private void ChangePosition()
        {
            int newPosition;

            do
            {
                int angle = 360 / NumSpawnPositions;
                int multiplier = UnityEngine.Random.Range(0, NumSpawnPositions);

                newPosition = angle * multiplier;
            }
            while (newPosition == (int)transform.eulerAngles.z);

            Quaternion newRotation = Quaternion.Euler(0f, 0f, newPosition);
            transform.rotation = newRotation;

            PlayAppearAnimation();
        }

        private void PlayAppearAnimation()
        {
            Animator.Play("Appear", -1, 0f);
        }
    }
}