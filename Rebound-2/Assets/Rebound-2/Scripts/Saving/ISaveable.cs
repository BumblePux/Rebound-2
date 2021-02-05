using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.Saving
{
    public interface ISaveable
    {
        void CaptureState(SaveData data);
        void RestoreState(SaveData data);
    }
}