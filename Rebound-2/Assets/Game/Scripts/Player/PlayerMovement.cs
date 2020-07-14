using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Managers;
using BumblePux.Rebound.Triggers;
using BumblePux.Tools;
using System;
using UnityEngine;

namespace BumblePux.Rebound.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public enum Rotation { Clockwise, AntiClockwise };

        [Header("Movement Settings")]
        public float Speed = 50f;
        public Rotation Direction = Rotation.AntiClockwise;
        public Transform OrbitTarget;
        public float TargetChangeCorrectionDuration = 0.2f;

        [Header("Planet Change Settings")]
        public bool AlwaysChangePlanet = false;

        private float playerOffsetFromPlanet;
        private bool isCorrecting;
        private float correctionStartTime;

        private GameModeBase gameMode;
        private Transform sprite;

        private bool isInitialized;


        public void Initialize()
        {
            gameMode = GameplayStatics.GetGameMode();
            sprite = GetComponentInChildren<SpriteRenderer>().transform;

            playerOffsetFromPlanet = gameMode.PlayerOffset;

            SetTarget(PlanetsManager.Instance.GetRandomActivePlanet());

            transform.position = new Vector3(OrbitTarget.position.x + playerOffsetFromPlanet, OrbitTarget.position.y, OrbitTarget.position.z);

            isInitialized = true;
        }

        public void SetTarget(Transform newTarget)
        {
            OrbitTarget = newTarget;
            isCorrecting = true;
            correctionStartTime = Time.time;
        }

        public void ChangeDirection(bool flipSprite)
        {
            Direction = Direction == Rotation.Clockwise ? Rotation.AntiClockwise : Rotation.Clockwise;

            if (flipSprite)
            {
                Vector3 newScale = new Vector3(sprite.localScale.x, sprite.localScale.y * -1, sprite.localScale.z);
                sprite.localScale = newScale;
            }
        }

        public void TryChangeDirection(bool flipSprite)
        {
            if (Utils.CoinToss())
            {
                ChangeDirection(flipSprite);
            }
        }

        private void Update()
        {
            if (!isInitialized) return;

            if (!gameMode.IsGameOver && gameMode.HasGameStarted)
            {
                Rotate();
            }

            if (isCorrecting)
            {
                Correct();
            }
        }

        private void Rotate()
        {
            if (Direction == Rotation.Clockwise)
                transform.RotateAround(OrbitTarget.position, Vector3.forward, -Speed * Time.deltaTime);
            else
                transform.RotateAround(OrbitTarget.position, Vector3.forward, Speed * Time.deltaTime);
        }

        private void Correct()
        {
            float percentage = (Time.time - correctionStartTime) / TargetChangeCorrectionDuration;

            CorrectOffset(percentage);
            CorrectRotation(percentage);

            if (percentage >= 1f)
                isCorrecting = false;
        }

        private void CorrectOffset(float t)
        {
            Vector3 newPosition = (transform.position - OrbitTarget.position).normalized * playerOffsetFromPlanet + OrbitTarget.position;
            transform.position = Vector3.Lerp(transform.position, newPosition, t);
        }

        private void CorrectRotation(float t)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(OrbitTarget.position - transform.position, transform.TransformDirection(Vector3.up));
            Quaternion newRotation = new Quaternion(0f, 0f, desiredRotation.z, desiredRotation.w);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, t);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var point = other.GetComponent<TransitionPoint>();
            if (point)
            {
                TryChangePlanet(point);
            }
        }

        private void TryChangePlanet(TransitionPoint point)
        {
            if (Utils.CoinToss() || AlwaysChangePlanet)
            {
                var linkedPlanets = point.GetLinkedPlanets();
                var newTarget = GetNewTarget(linkedPlanets);
                SetTarget(newTarget);
                ChangeDirection(false);
            }
        }

        private Transform GetNewTarget(Transform[] linkedPlanets)
        {
            if (OrbitTarget == linkedPlanets[0])
                return linkedPlanets[1];
            else
                return linkedPlanets[0];
        }
    }
}