using System;
using UnityEngine;

namespace BumblePux.Rebound2.Gameplay
{
    public class Counter : MonoBehaviour
    {
        public event Action<int> OnValueChanged;

        [Header("Data")]
        [SerializeField] private int CurrentValue = default;

        public int Value
        {
            get => CurrentValue;
            set
            {
                CurrentValue = value;
                OnValueChanged?.Invoke(CurrentValue);
            }
        }
    }
}