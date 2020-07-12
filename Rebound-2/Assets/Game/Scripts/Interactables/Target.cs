using BumblePux.Rebound.GameModes;
using BumblePux.Rebound.Managers;
using UnityEngine;

namespace BumblePux.Rebound.Interactables
{
    public class Target : MonoBehaviour, IInteractable
    {
        public int PointValue = 1;
        public int SpawnPoints = 6;

        private Transform parentPlanet;
        private int currentPosition;

        private GameModeBase gameMode;



        public void Initialize()
        {
            gameMode = GameManager.Instance.CurrentGameMode;

            parentPlanet = transform.parent;
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
            while (newPosition == currentPosition);

            currentPosition = newPosition;

            transform.RotateAround(parentPlanet.position, Vector3.forward, currentPosition);
        }
    }
}