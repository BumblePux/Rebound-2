using UnityEngine;

namespace BumblePux.Tools
{
    public static class Utils
    {
        /// <summary>
        /// Will randomly return true or false. Simulating the flip of a coin.
        /// </summary>
        /// <returns></returns>
        public static bool CoinToss()
        {
            int flip = Random.Range(0, 2);
            if (flip == 1)
                return true;
            else
                return false;
        }
    }
}