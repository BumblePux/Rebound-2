using BumblePux.Rebound.GameModes;
using UnityEngine;

namespace BumblePux.Rebound.Interactables
{
    public class Target : Actor, IInteractable
    {
        [Header("Settings")]
        public int PointValue = 1;
        public int SpawnPoints = 6;

        private Transform parentPlanet;
        private float offset;

        private Transform sprite;



        public void Initialize()
        {
            sprite = GetComponentInChildren<SpriteRenderer>().transform;

            offset = GetGameMode().PlayerOffset;

            parentPlanet = transform.parent;

            transform.position = parentPlanet.position;
            sprite.localPosition = new Vector3(offset, 0f, 0f);

            ChangePosition();
        }

        public void Interact()
        {
            GetGameMode().TargetHit(this);

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