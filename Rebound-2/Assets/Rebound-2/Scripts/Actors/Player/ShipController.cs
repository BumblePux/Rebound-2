using BumblePux.Rebound2.Input;
using BumblePux.Rebound2.Utils;
using System;
using UnityEngine;

namespace BumblePux.Rebound2.Actors.Player
{
    [RequireComponent(typeof(ShipMovement))]
    [RequireComponent(typeof(ShipVisuals))]
    [RequireComponent(typeof(ShipInteraction))]
    public class ShipController : MonoBehaviour
    {
        public event Action OnInteractMissed;

        [Header("Required References")]
        [SerializeField] private ShipMovement movement = default;
        [SerializeField] private ShipVisuals visuals = default;
        [SerializeField] private ShipInteraction interaction = default;
        [SerializeField] private PlayerInput input = default;

        [Header("Settings")]
        public bool CanMove = true;

        [Header("Debug")]
        public bool AlwaysChangePlanet;
        public bool IsInitialized;


        public void Initialize(Transform startPlanet, float distanceFromPlanet, float startSpeed)
        {
            visuals.EnableTrail(false);

            movement.SetOrbitTarget(startPlanet);
            movement.MoveClockwise = false;
            movement.Speed = startSpeed;
            movement.OffsetFromTarget = distanceFromPlanet;

            visuals.ResetToDefault();
            visuals.EnableTrail(true);

            IsInitialized = true;
        }

        private void Awake()
        {
            movement = GetComponent<ShipMovement>();
            visuals = GetComponent<ShipVisuals>();
            interaction = GetComponent<ShipInteraction>();
            input = GetComponent<PlayerInput>();
        }

        private void Update()
        {
            if (!IsInitialized) return;
            if (!CanMove) return;

            if (input.Interact())
            {
                interaction.TryInteract((success) =>
                {
                    if (!success)
                        OnInteractMissed?.Invoke();
                });
            }

            movement.Move();
        }

        public void SetMoveSpeed(float newSpeed)
        {
            movement.Speed = newSpeed;
        }

        public void ChangePlanet(Transform planet)
        {
            movement.SetOrbitTarget(planet);
        }

        public void ChangeDirection(bool flipSprite)
        {
            movement.MoveClockwise = !movement.MoveClockwise;
            if (flipSprite)
                visuals.Flip();
        }

        public void TryChangeDirection(bool flipSprite)
        {
            if (GameUtils.CoinFlip())
                ChangeDirection(flipSprite);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("TransitionPoint"))
            {
                var point = other.GetComponent<TransitionPoint>();
                if (point != null)
                    TryChangePlanet(point);
            }
        }

        private void TryChangePlanet(TransitionPoint point)
        {
            if (GameUtils.CoinFlip() || AlwaysChangePlanet)
            {
                var linkedPlanets = point.GetLinkedPlanets();
                var newTarget = GetNewPlanet(linkedPlanets);
                ChangePlanet(newTarget);
                ChangeDirection(false);
            }
        }

        private Transform GetNewPlanet(Transform[] linkedPlanets)
        {
            if (movement.OrbitTarget == linkedPlanets[0])
                return linkedPlanets[1];
            else
                return linkedPlanets[0];
        }
    }
}