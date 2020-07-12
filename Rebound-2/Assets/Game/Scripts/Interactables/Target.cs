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
        private float offset;

        private GameModeBase gameMode;



        public void Initialize()
        {
            gameMode = GameManager.Instance.CurrentGameMode;
            offset = gameMode.PlayerOffset;

            parentPlanet = transform.parent;

            transform.position = new Vector3(parentPlanet.position.x + offset, parentPlanet.position.y, parentPlanet.position.z);

            //while (currentPosition == 0)
            //    ChangePosition();            
        }

        public void Interact()
        {
            gameMode.TargetHit(this);

            // Play VFX
            // Play SFX

            ChangePosition();
            //SimpleChangePosition();
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

            //Debug.Log($"[Target] Current: {currentPosition}, New: {newPosition}");

            currentPosition = newPosition;

            transform.RotateAround(parentPlanet.localPosition, Vector3.forward, currentPosition);
        }

        private void SimpleChangePosition()
        {
            int randomAngle = Random.Range(0, 360);

            transform.RotateAround(parentPlanet.localPosition, Vector3.forward, randomAngle);
        }
    }
}