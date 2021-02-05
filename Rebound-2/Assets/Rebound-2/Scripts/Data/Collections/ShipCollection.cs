using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BumblePux.Rebound2.Data.Collections
{
    [CreateAssetMenu(fileName = "New Ship Collection", menuName = "Data/Collections/Ships")]
    public class ShipCollection : ScriptableObject
    {
        public List<ShipData> Ships = new List<ShipData>();
    }
}