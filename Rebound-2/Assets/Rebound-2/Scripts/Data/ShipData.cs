using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.Data
{
    [CreateAssetMenu(fileName = "New Ship Data", menuName = "Data/Ship")]
    public class ShipData : ScriptableObject
    {
        public string Name;
        public Sprite ShipPreviewSprite;
        public GameObject ShipModelPrefab;
        public bool IsUnlocked;
        public int Cost;
    }
}