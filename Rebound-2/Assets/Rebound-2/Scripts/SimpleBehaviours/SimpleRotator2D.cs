using BumblePux.Rebound2.Utils;
using UnityEngine;

namespace BumblePux.Rebound2.SimpleBehaviours
{
    public class SimpleRotator2D : MonoBehaviour
    {
        [Header("Speed Settings")]
        public int Speed = 1;
        public int MinSpeed = 1;
        public int MaxSpeed = 2;
        public bool MoveClockwise;

        [Header("Options")]
        [Tooltip("If enabled, Speed will be randomly selected on Start, between MinSpeed [Inclusive] and MaxSpeed [Inclusive].")]
        public bool UseRandomSpeed;
        [Tooltip("If enabled, MoveClockwise will be randomly be set on Start.")]
        public bool UseRandomDirection;

        private float randomSpeed;

        private void Start()
        {
            if (UseRandomSpeed)
                Speed = Random.Range(MinSpeed, MaxSpeed + 1);

            if (UseRandomDirection)
                MoveClockwise = GameUtils.CoinFlip();
        }

        private void Update()
        {
            Rotate();
        }

        private void Rotate()
        {
            if (MoveClockwise)
                transform.Rotate(Vector3.forward, -Speed * Time.deltaTime, Space.Self);
            else
                transform.Rotate(Vector3.forward, Speed * Time.deltaTime, Space.Self);
        }
    }
}