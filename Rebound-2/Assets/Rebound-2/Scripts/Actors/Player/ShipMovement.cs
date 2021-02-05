using UnityEngine;

namespace BumblePux.Rebound2.Actors.Player
{
    public class ShipMovement : MonoBehaviour
    {
        [Header("Settings")]
        public float Speed = 50f;
        public bool MoveClockwise;
        public Transform OrbitTarget;
        public float TargetChangeCorrectionDuration = 0.2f;

        [HideInInspector]
        public float OffsetFromTarget;

        private bool isCorrecting;
        private float correctionStartTime;

        public void Move()
        {
            if (MoveClockwise)
                transform.RotateAround(OrbitTarget.position, Vector3.forward, -Speed * Time.deltaTime);
            else
                transform.RotateAround(OrbitTarget.position, Vector3.forward, Speed * Time.deltaTime);
        }

        public void SetOrbitTarget(Transform target)
        {
            OrbitTarget = target;
            isCorrecting = true;
            correctionStartTime = Time.time;
        }

        private void Update()
        {
            CorrectPosition();
        }

        private void CorrectPosition()
        {
            if (!isCorrecting) return;

            float percentage = (Time.time - correctionStartTime) / TargetChangeCorrectionDuration;
            CorrectOffset(percentage);
            CorrectRotation(percentage);

            if (percentage >= 1f) 
                isCorrecting = false;
        }

        private void CorrectOffset(float t)
        {
            Vector3 newPosition = (transform.position - OrbitTarget.position).normalized * OffsetFromTarget + OrbitTarget.position;
            transform.position = Vector3.Lerp(transform.position, newPosition, t);
        }

        private void CorrectRotation(float t)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(OrbitTarget.position - transform.position, transform.TransformDirection(Vector3.up));
            Quaternion newRotation = new Quaternion(0f, 0f, desiredRotation.z, desiredRotation.w);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, t);
        }
    }
}