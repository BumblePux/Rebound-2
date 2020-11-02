using BumblePux.Rebound.Actors.Interactables;
using BumblePux.Rebound.Data;
using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Utils;
using UnityEngine;

namespace BumblePux.Rebound.Actors
{
    public class Player : MonoBehaviour
    {
        public enum Rotation { Clockwise, AntiClockwise }
        public enum State { Idle, Moving }

        [Header("Movement Settings")]
        public float Speed = 50f;
        public Rotation Direction = Rotation.AntiClockwise;
        public Transform OrbitTarget;
        public float TargetChangeCorrectionDuration = 0.2f;

        [Header("Player State")]
        public State CurrentState = State.Moving;

        [Header("Debug")]
        public bool AlwaysChangePlanet = false;
        public bool AutoInteract = false;

        // References
        private PlayerInput input;
        private ReboundGameMode gameMode;
        private GameData gameData;

        private GameObject ship;
        private Transform sprite = default;
        private TrailRenderer trail = default;
        private IInteractable interactable;

        // Variables
        private bool isCorrecting = false;
        private float correctionStartTime = 0f;
        private float offsetFromTarget = 3.5f;        

        //---------------------------------------------------------------------------
        // Public Methods
        //---------------------------------------------------------------------------
        public void SetStartPosition(Transform target = null)
        {
            if (trail != null) trail.enabled = false;

            ResetDefaultDirection();

            transform.position = Vector3.right;
            transform.rotation = Quaternion.Euler(Vector3.zero);

            if (target != null)
                SetTarget(target);

            if (OrbitTarget == null)
            {
                transform.position = new Vector3(offsetFromTarget, 0f, 0f);
            }
            else
            {
                transform.position = OrbitTarget.position + Vector3.right;
                transform.position = (transform.position - OrbitTarget.position).normalized * offsetFromTarget + OrbitTarget.position;
            }

            if (trail != null) trail.enabled = true;
        }

        //--------------------------------------------------
        public void ResetDefaultDirection()
        {
            Direction = Rotation.AntiClockwise;
            Vector3 defaultScale = new Vector3(Mathf.Abs(sprite.localScale.x), Mathf.Abs(sprite.localScale.y), Mathf.Abs(sprite.localScale.z));
            sprite.localScale = defaultScale;
        }

        //--------------------------------------------------
        public void SetTarget(Transform newTarget)
        {
            OrbitTarget = newTarget;
            isCorrecting = true;
            correctionStartTime = Time.time;
        }

        //--------------------------------------------------
        public void ChangeDirection(bool flipSprite)
        {
            Direction = Direction == Rotation.Clockwise ? Rotation.AntiClockwise : Rotation.Clockwise;

            if (flipSprite)
            {
                Vector3 newScale = new Vector3(sprite.localScale.x, sprite.localScale.y * -1, sprite.localScale.z);
                sprite.localScale = newScale;
            }
        }

        //--------------------------------------------------
        public void TryChangeDirection(bool flipSprite)
        {
            if (GameUtils.GetRandomBool())
            {
                ChangeDirection(flipSprite);
            }
        }

        //---------------------------------------------------------------------------
        // Unity START
        //---------------------------------------------------------------------------
        private void Start()
        {
            //Instantiate(GetGameData().CurrentShip.ShipPrefab, transform);

            gameData = GameUtils.GetGameData();
            if (transform.childCount != 0)
            {
                foreach (Transform child in transform)
                {
                    Destroy(child.gameObject);
                }
            }

            ship = Instantiate(gameData.ActiveShip.ShipPrefab, transform);

            gameMode = GameUtils.GetGameMode();
            offsetFromTarget = gameMode.PlayerDistanceFromPlanet;

            input = GetComponent<PlayerInput>();

            //sprite = GetComponentInChildren<SpriteRenderer>().transform;
            sprite = ship.GetComponent<SpriteRenderer>().transform;
            trail = GetComponentInChildren<TrailRenderer>();

            SetStartPosition();
        }

        //---------------------------------------------------------------------------
        // Unity UPDATE
        //---------------------------------------------------------------------------
        private void Update()
        {
            switch (CurrentState)
            {
                case State.Idle:
                    HandleCorrection();
                    break;

                case State.Moving:
                    Rotate();
                    HandleCorrection();
                    HandleInteraction();
                    break;

                default:
                    break;
            }
        }

        //---------------------------------------------------------------------------
        // Movement
        //---------------------------------------------------------------------------
        private void Rotate()
        {
            if (Direction == Rotation.Clockwise)
                transform.RotateAround(OrbitTarget.position, Vector3.forward, -Speed * Time.deltaTime);
            else
                transform.RotateAround(OrbitTarget.position, Vector3.forward, Speed * Time.deltaTime);
        }

        //--------------------------------------------------
        private void HandleCorrection()
        {
            if (!isCorrecting) return;

            // Don't attempt to correct if there's no target for some reason
            if (OrbitTarget == null)
            {
                isCorrecting = false;
                return;
            }

            float percentage = (Time.time - correctionStartTime) / TargetChangeCorrectionDuration;
            CorrectOffset(percentage);
            CorrectRotation(percentage);

            if (percentage >= 1f)
                isCorrecting = false;
        }

        //--------------------------------------------------
        private void CorrectOffset(float t)
        {
            Vector3 newPosition = (transform.position - OrbitTarget.position).normalized * offsetFromTarget + OrbitTarget.position;
            transform.position = Vector3.Lerp(transform.position, newPosition, t);
        }

        //--------------------------------------------------
        private void CorrectRotation(float t)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(OrbitTarget.position - transform.position, transform.TransformDirection(Vector3.up));
            Quaternion newRotation = new Quaternion(0f, 0f, desiredRotation.z, desiredRotation.w);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, t);
        }

        //---------------------------------------------------------------------------
        // Unity TRIGGERS
        //---------------------------------------------------------------------------
        private void OnTriggerEnter2D(Collider2D other)
        {
            var point = other.GetComponent<TransitionPoint>();
            if (point)
            {
                TryChangePlanet(point);
            }
            else
            {
                interactable = other.GetComponentInParent<IInteractable>();
                if (interactable != null && AutoInteract)
                {
                    TryInteract();
                }
            }
        }

        //--------------------------------------------------
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponentInParent<IInteractable>() != null)
                interactable = null;
        }

        //---------------------------------------------------------------------------
        // Interaction
        //---------------------------------------------------------------------------
        private void HandleInteraction()
        {
            if (input.Interact())
            {
                TryInteract();
            }
        }

        //--------------------------------------------------
        private void TryInteract()
        {
            if (interactable != null)
                interactable.Interact();
            else
                gameMode.StarMissed();
        }

        //---------------------------------------------------------------------------
        // Planet Switching
        //---------------------------------------------------------------------------
        private void TryChangePlanet(TransitionPoint point)
        {
            if (GameUtils.GetRandomBool() || AlwaysChangePlanet)
            {
                var linkedPlanets = point.GetLinkedPlanets();
                var newTarget = GetNewTarget(linkedPlanets);

                SetTarget(newTarget);
                ChangeDirection(false);
            }
        }

        //--------------------------------------------------
        private Transform GetNewTarget(Transform[] linkedPlanets)
        {
            if (OrbitTarget == linkedPlanets[0])
                return linkedPlanets[1];
            else
                return linkedPlanets[0];
        }
    }
}