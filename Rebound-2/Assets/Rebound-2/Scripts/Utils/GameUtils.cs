using UnityEngine;

namespace BumblePux.Rebound2.Utils
{
    public class GameUtils : MonoBehaviour
    {
        public static bool CoinFlip()
        {
            int random = Random.Range(0, 2);
            return random == 1 ? true : false;
        }
    }
}