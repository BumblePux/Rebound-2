using System;
using UnityEngine;

namespace BumblePux.Rebound2.Gameplay
{
    public class Timer : MonoBehaviour
    {
        public event Action<float> OnRemainingTimeChanged;

        [Header("Data")]
        public float MaxTime;
        public float RemainingTime;

        [Header("Debug")]
        public bool HasElapsed;
        public bool IsRunning;

        public void Begin(float startTime)
        {
            MaxTime = startTime;
            RemainingTime = MaxTime;
            IsRunning = false;
            HasElapsed = false;

            OnRemainingTimeChanged?.Invoke(RemainingTime);
        }

        public void Resume()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
        }

        public void IncreaseRemainingTime(float amount)
        {
            RemainingTime = Mathf.Clamp(RemainingTime + amount, 0f, MaxTime);
        }

        public void DecreaseRemainingTime(float amount)
        {
            RemainingTime = Mathf.Clamp(RemainingTime - amount, 0f, MaxTime);
        }

        private void Update()
        {
            if (IsRunning && !HasElapsed)
            {
                RemainingTime -= Time.deltaTime;

                if (RemainingTime <= float.Epsilon)
                {
                    RemainingTime = 0f;
                    HasElapsed = true;
                }

                OnRemainingTimeChanged?.Invoke(RemainingTime);
            }
        }
    }
}