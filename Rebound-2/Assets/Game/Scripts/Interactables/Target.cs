using BumblePux.Rebound.GameModes;
using UnityEngine;

namespace BumblePux.Rebound.Interactables
{
    public class Target : MonoBehaviour, IInteractable
    {
        [Header("Settings")]
        public int PointValue = 1;
        public int SpawnPoints = 6;

        private Transform parentPlanet;
        private float offset;

        private GameModeBase gameMode;
        private Transform sprite;



        public void Initialize()
        {
            gameMode = GameplayStatics.GetGameMode();
            sprite = GetComponentInChildren<SpriteRenderer>().transform;

            offset = gameMode.PlayerOffset;

            parentPlanet = transform.parent;

            transform.position = parentPlanet.position;
            sprite.localPosition = new Vector3(offset, 0f, 0f);

            ChangePosition();
        }

        public void Interact()
        {
            gameMode.TargetHit(this);

            // Play VFX
            // Play SFX

            ChangePosition();
        }

        private void ChangePosition()
        {
            int newPosition;

            do
            {
                int angle = 360 / SpawnPoints;
                int multiplier = Random.Range(0, SpawnPoints);

                newPosition = angle * multiplier;
            }
            while (newPosition == (int)transform.localEulerAngles.z);

            //Debug.Log($"[Target] Current: {transform.localEulerAngles.z}, New: {newPosition}");

            Quaternion newRotation = Quaternion.Euler(0f, 0f, newPosition);

            transform.rotation = newRotation;
        }
    }
}