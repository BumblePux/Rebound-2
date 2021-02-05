using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.Saving
{
    public class Saveable : MonoBehaviour
    {
        public void CaptureState(SaveData data)
        {
            var saveables = GetComponents<ISaveable>();
            if (saveables.Length == 0) return;

            foreach(var saveable in saveables)
            {
                saveable.CaptureState(data);
            }
        }

        public void RestoreState(SaveData data)
        {
            var saveables = GetComponents<ISaveable>();
            if (saveables.Length == 0) return;

            foreach (var saveable in saveables)
            {
                saveable.RestoreState(data);
            }
        }
    }
}