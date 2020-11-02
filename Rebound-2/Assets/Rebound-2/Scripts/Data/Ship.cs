using UnityEngine;

namespace BumblePux.Rebound.Data
{
    [CreateAssetMenu(fileName = "New Ship", menuName = "Game Data/New Ship")]
    public class Ship : ScriptableObject
    {
        public string Name;
        public GameObject ShipPrefab;
        public bool IsUnlocked;

		//--------------------------------------------------------------------------------
    }
}