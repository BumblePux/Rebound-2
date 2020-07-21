using UnityEngine;

namespace BumblePux.Rebound.Unlockables
{
    [CreateAssetMenu(fileName = "New Unlockable", menuName = "Unlockables/Unlockable")]
    public class Unlockable : ScriptableObject
    {
        public string Name;
        public GameObject Prefab;
        public Sprite PreviewImage;
        public bool IsUnlocked;
    }
}