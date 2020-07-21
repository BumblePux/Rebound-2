using UnityEngine;

namespace BumblePux.Rebound.Unlockables
{
    [CreateAssetMenu(fileName = "New Unlock Dataset", menuName = "Unlockables/New Dataset")]
    public class UnlockDataSet : ScriptableObject
    {
        public Unlockable[] Unlockables;
    }
}